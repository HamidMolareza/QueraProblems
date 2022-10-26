// Copy from Quera

// In the name of God
// the answer is equal to the sum of differences with the mean divided by two

#include<iostream>
#include<stdlib.h>
using namespace std;

int n , sum , mean , answer;

void go(int repeat){
	if(!repeat){
		mean = sum / n;
		return;
	}	
	int x;
	cin >> x;
	sum += x;
	go(repeat-1);
	answer = answer + abs(x - mean);
}

int main(){
	cin >> n;
	go(n);
	cout << answer/2 << '\n';
	return 0;
}
