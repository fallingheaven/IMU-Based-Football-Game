# 这是一个示例 Python 脚本。

import socket
import binascii
import asyncio

import numpy as np
from pygame.time import Clock
from bleak import BleakClient
from threading import Thread

# 写数据以及接受数据特征值
write_CHARACTERISTIC_UUID = "91680002-1111-6666-8888-0123456789ab"
notify_CHARACTERISTIC_UUID = "91680003-1111-6666-8888-0123456789ab"

# IMU的获取地址
imu_devices = [
    # {"address": "03:85:14:03:1F:B6", "name": "0722"},
    # #{"address": "03:85:14:03:1D:1A", "name": "0276"},
    # #{"address": "03:85:14:03:1C:E3", "name": "0273"},
    # {"address": "03:85:14:03:1C:CC", "name": "1006"},
    # {"address": "03:85:14:03:1F:25", "name": "0556"},
    {"address": "03:85:14:03:1C:87", "name": "0584"},
    # {"address": "03:85:14:03:94:25", "name": "0184"},
    # imu的蓝牙地址和名称
]
clock = Clock()
BUFFER_SIZE = 500
first_transition = True


# IMU 16进制转十进制
def HexSting2decimal(a):
    ints = 0xFFFF
    to = int(str(a), 16)
    if to > 32753:
        to = to - ints - 1
    return to


class ble_server():

    def __init__(self, address, name, target):
        # def __init__(self, address, name):
        self.address = address
        self.name = name
        self.disconnect_num = 0
        self.running = True
        self.imu_data_list = []
        self.fps_data = []
        self.data_buffer = []

        [self.v_x, self.v_y, self.v_z] = [0, 0, 0]
        # self.current_time = float(0)
        # self.last_time = float(0)
        [self.offset_v_x, self.offset_v_y, self.offset_v_z] = [0, 0, 0]
        [self.last_acc_x, self.last_acc_y, self.last_acc_z] = [0, 0, 0]

        self.target_server = target

    async def show_all(self, address):
        async with BleakClient(address) as client:
            print(f"{self.name}  Connected: {client.is_connected}")

    async def main(self, address, write_char_uuid, notify_char_uuid):
        def notification_handler(sender, data):
            # 接受硬件返回的数据
            """Simple notification handler which prints the data received."""
            # print("接收数据")
            # print(data)
            data = str(binascii.b2a_hex(data), 'utf-8')
            # 解码格式可能会改变

            # print(data)

            if len(data) != 84:
                # 错误数据会打印出来
                print(f"{self.name}:error data: {data}")
            else:

                self.data_buffer.append(data)
                if len(self.data_buffer) > BUFFER_SIZE:
                    self.data_buffer.pop(0)

            self.data_transition()

        def disconnected_callback(client):
            print("Disconnected callback called!")
            disconnected_event.set()

        disconnected_event = asyncio.Event()

        # while self.running:
        try:
            self.disconnect_num += 1
            client = BleakClient(address, disconnected_callback=disconnected_callback, mtu_size=200)
            await client.connect()
            print(f"Connected: {client.is_connected}")
            d_name = "AB01FFFFFF"  # 不用改

            await client.write_gatt_char(write_char_uuid, bytes.fromhex(d_name))

            await client.start_notify(notify_char_uuid, notification_handler)

            await asyncio.sleep(10)
            # self.draw()

            await disconnected_event.wait()
        except Exception as e:
            print(e)
            print("Disconnect Num: ", self.disconnect_num)

    def data_transition(self):
        data = self.get_latest_data()
        # head = data[0:4]
        # size = int(data[4:6], 16)
        # battery = int(data[6:8], 16)
        quat_w = (HexSting2decimal(data[10:12] + data[8:10])) / 10000
        quat_x = (HexSting2decimal(data[14:16] + data[12:14])) / 10000
        quat_y = (HexSting2decimal(data[18:20] + data[16:18])) / 10000
        quat_z = (HexSting2decimal(data[22:24] + data[20:22])) / 10000
        # angle_x = (HexSting2decimal(data[26:28] + data[24:26])) / 100
        # angle_y = (HexSting2decimal(data[30:32] + data[28:30])) / 100
        # angle_z = (HexSting2decimal(data[34:36] + data[32:34])) / 100
        acc_x = (HexSting2decimal(data[38:40] + data[36:38])) / 100
        acc_y = (HexSting2decimal(data[42:44] + data[40:42])) / 100
        acc_z = (HexSting2decimal(data[46:48] + data[44:46])) / 100
        # angvel_x = (HexSting2decimal(data[50:52] + data[48:50])) / 80
        # angvel_y = (HexSting2decimal(data[54:56] + data[52:54])) / 80
        # angvel_z = (HexSting2decimal(data[58:60] + data[56:58])) / 80
        # geomag_x = (HexSting2decimal(data[62:64] + data[60:62])) * 100
        # geomag_y = (HexSting2decimal(data[66:68] + data[64:66])) * 100
        # geomag_z = (HexSting2decimal(data[70:72] + data[68:70])) * 100
        # tran_x = (HexSting2decimal(data[74:76] + data[72:74])) / 1000
        # tran_y = (HexSting2decimal(data[78:80] + data[76:78])) / 1000
        # tran_z = (HexSting2decimal(data[82:84] + data[80:82])) / 1000
        # tran=[tran_x,tran_y,tran_z]

        p = quaternion2matrix([quat_w, quat_x, quat_y, quat_z])
        p_inv = np.linalg.inv(p)

        real_acc = np.dot(p_inv, np.array([[acc_x],
                                           [acc_y],
                                           [acc_z]]))
        # 去除初始加速度
        global first_transition
        if first_transition and real_acc[2] > 100:
            [self.offset_v_x, self.offset_v_y, self.offset_v_z] = [real_acc[0][0], real_acc[1][0], real_acc[2][0]]
            # print([self.offset_v_x, self.offset_v_y, self.offset_v_z])

        delta_time = 0.001
        # 如果加速度足够大就积分
        real_acc -= [[self.offset_v_x], [self.offset_v_y], [self.offset_v_z]]
        if np.sqrt(real_acc[0] ** 2 + real_acc[1] ** 2 + real_acc[2] ** 2) > 1:
            [self.v_x, self.v_y, self.v_z] = [self.v_x + delta_time * (real_acc[0][0] + self.last_acc_x) / 2,
                                              self.v_y + delta_time * (real_acc[1][0] + self.last_acc_y) / 2,
                                              self.v_z + delta_time * (real_acc[2][0] + self.last_acc_z) / 2]
            [self.last_acc_x, self.last_acc_y, self.last_acc_z] = [real_acc[0][0], real_acc[1][0], real_acc[2][0]]
        else:
            [self.last_acc_x, self.last_acc_y, self.last_acc_z] = [0, 0, 0]
        # 手动调试速度
        if np.sqrt(self.v_x ** 2 + self.v_y ** 2 + self.v_z ** 2) < 0.75:
            [self.v_x, self.v_y, self.v_z] = [0, 0, 0]
        # 向unity传输数据
        if self.target_server:
            data_to_send = f"{self.v_x}, {self.v_y}, {self.v_z}, {quat_x}, {quat_y}, {quat_z}, {quat_w}, "
            self.target_server.sendall(data_to_send.encode())

    def get_latest_data(self):
        return self.data_buffer[-1] if self.data_buffer else None

    def start(self):
        print("蓝牙" + self.name + "连接中...")
        asyncio.run(self.main(self.address, write_CHARACTERISTIC_UUID, notify_CHARACTERISTIC_UUID))

        self.data_transition()


def quaternion2matrix(q):
    # q:list
    w, x, y, z = q
    return np.array([[1 - 2 * y * y - 2 * z * z, 2 * x * y - 2 * z * w, 2 * x * z + 2 * y * w],
                     [2 * x * y + 2 * z * w, 1 - 2 * x * x - 2 * z * z, 2 * y * z - 2 * x * w],
                     [2 * x * z - 2 * y * w, 2 * y * z + 2 * x * w, 1 - 2 * x * x - 2 * y * y]])


if __name__ == "__main__":
    t_pool = []
    imu_instances = []
    imu_order = []

    # 创建一个服务器 Socket
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    # 绑定 IP 地址和端口
    server_address = ('127.0.0.1', 8080)  # 这里可以使用服务器的 IP 地址
    server_socket.bind(server_address)
    # 开始监听连接
    server_socket.listen(1)
    # 接受客户端连接
    client_socket, client_address = server_socket.accept()

    # 连接传感器
    for device_info in imu_devices:
        imu = ble_server(device_info["address"], device_info["name"], client_socket)
        # imu = ble_server(device_info["address"], device_info["name"])
        imu_instances.append(imu)
        imu_order.append(device_info["name"])
        t_pool.append(Thread(target=imu.start))

    print("imu的次序是：", imu_order)

    for t in t_pool:
        t.start()
