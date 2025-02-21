import re
from datetime import datetime

# Global variables
clock_map = {}
grand_clock_name = ""
grand_clock_time = ""

def parse_clock_data(file_path):
    global clock_map, grand_clock_name, grand_clock_time
    with open(file_path, 'r') as file:
        lines = file.readlines()
        for line in lines:
            line = line.strip()
            if line.startswith('- Clock'):
                match = re.match(r'- (Clock \d): (.*)', line)
                if match:
                    clock_name = match.group(1).strip()
                    clock_time = match.group(2).strip()
                    if clock_name and clock_time:
                        clock_map[clock_name] = clock_time
            elif line.startswith('The ') and ' is at ' in line:
                match = re.match(r'^The\s+(.+?)\s+is\s+at\s+(\d{1,2}:\d{2})\.$', line)
                if match:
                    grand_clock_name = match.group(1).strip()
                    grand_clock_time = match.group(2).strip()

def calculate_time_differences(clock_map):
    global grand_clock_time
    time_format = "%H:%M"
    grand_clock = datetime.strptime(grand_clock_time, time_format)
    time_differences = []

    for clock_name, clock_time in clock_map.items():
        clock_time_dt = datetime.strptime(clock_time, time_format)
        time_difference = (clock_time_dt - grand_clock).total_seconds() / 60
        time_differences.append(int(time_difference))

    return time_differences

if __name__ == "__main__":
    file_path = 'clockdata.txt'
    parse_clock_data(file_path)
    time_differences = calculate_time_differences(clock_map)
    print(time_differences)
