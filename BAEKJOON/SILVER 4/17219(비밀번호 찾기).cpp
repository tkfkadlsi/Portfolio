#include <iostream>
#include <unordered_map>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	unordered_map<string, string> passwordMap;

	int n, m;
	string url, password;

	cin >> n >> m;

	for (int i = 0; i < n; i++)
	{
		cin >> url >> password;
		passwordMap[url] = password;
	}

	for (int i = 0; i < m; i++)
	{
		cin >> url;
		cout << passwordMap[url] << '\n';
	}

	return 0;
}