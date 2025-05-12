#include <iostream>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n, divide = 5, answer = 0;

	cin >> n;

	while (n >= divide)
	{
		answer += n / divide;
		divide *= 5;
	}

	cout << answer;

	return 0;
}