import pyautogui
import time
import struct
import socket

integer_index = 2501
UDP_IP = "127.0.0.1"
UDP_PORT = 3615
time_between_sends = 0.04

    
def send_udp_integer(value):
    little_endian = struct.pack('<ii',integer_index, value)
    #print(f"Sending: {little_endian}")
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.sendto(little_endian, (UDP_IP, UDP_PORT))
    

def get_mouse_position():
    try:
        while True:
            x, y = pyautogui.position()
            screen_width, screen_height = pyautogui.size()
            percent_x = (x / screen_width) 
            percent_y = (y / screen_height) 
            percent_y = 1.0- percent_y
            int_x = int(percent_x*9999.0)
            int_y = int(percent_y*9999.0)
            if(int_x > 9999):
                int_x = 9999
            if(int_y > 9999):
                int_y = 9999
            if(int_x < 0):
                int_x = 0
            if(int_y < 0):
                int_y = 0
            value =1500000000 +  int(int_x* 10000.0) + int(int_y)
            print(f"X={percent_x:.2f}%, Y={percent_y:.2f}% Int: {value}")
            
            
            send_udp_integer(value)
            time.sleep(time_between_sends)
            
    except KeyboardInterrupt:
        print("\nProgram terminated.")

if __name__ == "__main__":
    get_mouse_position()