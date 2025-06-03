#include <iostream>
using namespace std;

int iArray[1000001] = { 0 };

int Func(int x)
{
	int divide3Count = 999999999;
	int divide2Count = 999999999;
	int minus1Count = 999999999;


	if (x == 1)
	{
		return 0;
	}
	if (iArray[x] != 0)
	{
		return iArray[x];
	}


	if (x % 3 == 0)
	{
		divide3Count = Func(x / 3);
		iArray[x / 3] = divide3Count;
	}

	if (x % 2 == 0)
	{
		divide2Count = Func(x / 2);
		iArray[x / 2] = divide2Count;
	}

	minus1Count = Func(x - 1);
	iArray[x - 1] = minus1Count;

	int min = 2100000000;

	if (divide3Count < min)
	{
		min = divide3Count;
	}
	if (divide2Count < min)
	{
		min = divide2Count;
	}
	if (minus1Count < min)
	{
		min = minus1Count;
	}

	return min + 1;
}


int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int x;

	cin >> x;

	cout << Func(x);

	return 0;
}