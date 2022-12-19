// Shayan Azizi
// https://github.com/shayan-azizi

#include <iostream>

using namespace std;

int main() {
  int n;
  string main_code;
  cin >> n >> main_code;

  string unique_chars = "";
  for (int i = 0; i < main_code.size(); i++) {
    bool is = true;
    for (int j = 0; j < unique_chars.size(); j++) {
      if (main_code[i] == unique_chars[j]) {
        is = false;
        break;
      }
    }
    if (is) {
      unique_chars += main_code[i];
    }
  }

  for (int i = 0; i < unique_chars.size(); i++) {
    for (int j = 0; j < unique_chars.size(); j++) {
      if (unique_chars[j] > unique_chars[j + 1]) {
        int c = unique_chars[j];
        unique_chars[j] = unique_chars[j + 1];
        unique_chars[j + 1] = c;
      }
    }
  }

  while (n--) {
    string code;
    cin >> code;
    string temp = "";

    for (int i = 0; i < code.size(); i++) {
      bool is = true;
      for (int j = 0; j < temp.size(); j++) {
        if (code[i] == temp[j]) {
          is = false;
          break;
        }
      }
      if (is) {
        temp += code[i];
      }
    }

    for (int i = 0; i < temp.size(); i++) {
      for (int j = 0; j < temp.size(); j++) {
        if (temp[j] > temp[j + 1]) {
          int c = temp[j];
          temp[j] = temp[j + 1];
          temp[j + 1] = c;
        }
      }
    }

    if (temp == unique_chars) {
      cout << "Yes" << endl;
    } else {
      cout << "No" << endl;
    }
  }

  return 0;
}