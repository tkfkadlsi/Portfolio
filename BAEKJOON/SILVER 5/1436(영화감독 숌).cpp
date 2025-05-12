#include <iostream>
#include <string>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	string sdummy;
	int n, idummy = 0, count = 0;

	cin >> n;

	while (n > count)
	{
		idummy++;
		sdummy = to_string(idummy);
		size_t nPos = sdummy.find("666");
		if (nPos != string::npos)
		{
			count++;
		}
	}

	cout << sdummy;

	return 0;
}