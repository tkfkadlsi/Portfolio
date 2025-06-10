#include <iostream>
#include <vector>
using namespace std;

int iArray[100001] = { 0 };

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n, m, a, b;

	cin >> n >> m;

	for (int i = 1; i <= n; i++)
	{
		cin >> a;
		iArray[i] = a + iArray[i - 1];
	}

	for (int i = 0; i < m; i++)
	{
		cin >> a >> b;
		cout << iArray[b] - iArray[a - 1] << '\n';
	}

	return 0;
}