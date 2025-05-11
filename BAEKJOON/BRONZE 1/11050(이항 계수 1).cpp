#include <iostream>
#include <algorithm>
using namespace std;

int facArray[11] = { 0 };

int factorial(int n)
{
	if (n == 0 || n == 1)
	{
		return 1;
	}
	else if (facArray[n] == 0)
	{
		facArray[n] = factorial(n - 1) * n;
	}

	return facArray[n];
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n, k;

	cin >> n >> k;

	cout << factorial(n) / (factorial(n - k) * factorial(k));

	return 0;
}