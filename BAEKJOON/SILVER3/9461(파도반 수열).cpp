#include <iostream>
using namespace std;

long long iArray[101] = { 0 };

long long padoban(long long i)
{
	if (iArray[i] != 0)
	{
		return iArray[i];
	}
	else
	{
		iArray[i] = padoban(i - 1) + padoban(i - 5);
		return iArray[i];
	}
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	iArray[1] = 1;
	iArray[2] = 1;
	iArray[3] = 1;
	iArray[4] = 2;
	iArray[5] = 2;

	int n, m;

	cin >> n;

	for (int i = 0; i < n; i++)
	{
		cin >> m;

		cout << padoban(m) << '\n';
	}

	return 0;
}