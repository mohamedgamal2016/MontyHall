#include <iostream>
#include <string>
using namespace std;

int main()
{
	int x, count = 0, arr[111] = { 0 }, targetPartition, totalSum = 0;
	cin >> x;
	char y;

	for (int i = 0; i < x; i++) {
		cin >> y;
		if (y == 'x') count++;
		else
		{
			if (count >= 3) arr[i] += count;
			count = 0;
		}
	}
	if (count >= 3) arr[x - 1] += count;

	for (int i = 0; i < 100; i++) {
		if (arr[i] != 0) {
			targetPartition = arr[i] - 2;
			totalSum += targetPartition;
		}
	}

	cout << totalSum;

	return 0;
}
