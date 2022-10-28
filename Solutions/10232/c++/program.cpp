//Copy from Quera

// In the name of God

#include<iostream>
using namespace std;

int main(){
	int n,l;
	cin >> n >> l;
	int current_length = 0, current_time = 0;
	for(int i=0;i<n;i++){
		int d,r,g;
		cin >> d >> r >> g;
		current_time += d - current_length;
		current_length = d;
		int temp = current_time%(r+g);
		if(temp < r)
			current_time += r - temp;
	}
	current_time += l - current_length;
	cout << current_time << '\n';
	return 0;
}
