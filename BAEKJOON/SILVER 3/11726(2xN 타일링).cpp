#include <iostream>
using namespace std;


int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int iArray[1001] = { 0 };
	iArray[1] = 1;
	iArray[2] = 2;

	int n;

	cin >> n;

	for (int i = 3; i <= n; i++)
	{
		iArray[i] = (iArray[i - 1] + iArray[i - 2]) % 10007;
	}

	cout << iArray[n];

	return 0;
}