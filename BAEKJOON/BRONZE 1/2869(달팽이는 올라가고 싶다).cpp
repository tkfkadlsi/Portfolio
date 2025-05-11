#include <iostream>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int a, b, v, day;

	cin >> a >> b >> v;

	v -= b;
	a -= b;

	day = v / a;

	if (v % a != 0)
	{
		day++;
	}

	cout << day;

	return 0;
}