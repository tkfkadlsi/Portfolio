#include <iostream>
using namespace std;

unsigned int lanCableArray[10000] = { 0 }, n, k;

bool IsConditionMet(unsigned int lenght)
{
	unsigned int cutedLanCableCount = 0;

	for (unsigned int i = 0; i < n; i++)
	{
		cutedLanCableCount += lanCableArray[i] / lenght;
	}

	return cutedLanCableCount >= k;
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	unsigned int low = 1, high = 1, mid = 0;

	cin >> n >> k;

	for (unsigned int i = 0; i < n; i++)
	{
		cin >> lanCableArray[i];
		if (lanCableArray[i] > high)
		{
			high = lanCableArray[i];
		}
	}

	while (low <= high)
	{
		mid = (low + high) / 2;

		if (IsConditionMet(mid))
		{
			low = mid + 1;
		}
		else
		{
			high = mid - 1;
		}
	}

	cout << (low + high) / 2;

	return 0;
}