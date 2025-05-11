#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n, input, array[10001] = { 0 };

	cin >> n;

	for (int i = 0; i < n; i++)
	{
		cin >> input;
		array[input]++;
	}

	for (int i = 1; i <= 10000; i++)
	{
		for (int j = 0; j < array[i]; j++)
		{
			cout << i << '\n';
		}
	}

	return 0;
}