#include <iostream>
using namespace std;

int iArray[20000001] = { 0 };

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n, m, input;

	cin >> n;

	for (int i = 0; i < n; i++)
	{
		cin >> input;
		iArray[input + 10000000]++;
	}

	cin >> m;

	for (int i = 0; i < m; i++)
	{
		cin >> input;
		cout << iArray[input + 10000000] << " ";
	}

	return 0;
}