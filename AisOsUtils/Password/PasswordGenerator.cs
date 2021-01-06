using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisOsUtils.Password
{
    public class PasswordGenerator
    {
		public static string GeneratePassword(DateTime date)
		{
			const string seed = "MPSJKMDHAI";
			var seedeight = seed.Substring(0, 8);
			var seedten = seed;

			int[][] table1 = {
				new[] { 15, 15, 24, 20, 24 },
				new[] {13, 14, 27, 32, 10},
				new[] {29, 14, 32, 29, 24},
				new[] {23, 32, 24, 29, 29},
				new[] {14, 29, 10, 21, 29},
				new[] {34, 27, 16, 23, 30},
				new[] {14, 22, 24, 17, 13}
			};

			int[][] table2 = {
				new[] {0, 1, 2, 9, 3, 4, 5, 6, 7, 8},
				new[] {1, 4, 3, 9, 0, 7, 8, 2, 5, 6},
				new[] {7, 2, 8, 9, 4, 1, 6, 0, 3, 5},
				new[] {6, 3, 5, 9, 1, 8, 2, 7, 4, 0},
				new[] {4, 7, 0, 9, 5, 2, 3, 1, 8, 6},
				new[] {5, 6, 1, 9, 8, 0, 4, 3, 2, 7}
			};

			char[] alphanum = {
				'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D',
				'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
				'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
			};

			var list1 = new int[10];
			var list2 = new int[10];
			var list3 = new int[10];
			var list4 = new int[10];
			var list5 = new int[10];
            // Now let's generate one password for each day
			// For each iteration advance the date one day		

			// Last two digits of the year		
			var year = date.Year % 100;

			// Number of the month (no leading zero; January == 0)
			var month = date.Month;

			// Day of the month
			var dayOfMonth = date.Day;

			// Day of the week. Normally 0 would be Sunday but we need it to be Monday.
			var dayOfWeek = (int)date.DayOfWeek - 1;
			if (dayOfWeek < 0)
			{
				dayOfWeek = 6;
			}

			// Now build the lists that will be used by each other.
			// list1
			for (int i = 0; i <= 4; i++)
			{
				list1[i] = table1[dayOfWeek][i];
			}

			list1[5] = dayOfMonth;
			if (((year + month) - dayOfMonth) < 0)
			{
				list1[6] = (((year + month) - dayOfMonth) + 36) % 36;
			}
			else
			{
				list1[6] = ((year + month) - dayOfMonth) % 36;
			}

			list1[7] = (((3 + ((year + month) % 12)) * dayOfMonth) % 37) % 36;

			// list2
			for (int i = 0; i <= 7; i++)
			{
				list2[i] = (seedeight.Substring(i, 1)[0]) % 36;
			}

			// list3
			for (int i = 0; i <= 7; i++)
			{
				list3[i] = (((list1[i] + list2[i])) % 36);
			}

			list3[8] = (list3[0] + list3[1] + list3[2] + list3[3] + list3[4] +
			list3[5] + list3[6] + list3[7]) % 36;
			int num8 = list3[8] % 6;
			list3[9] = (int)Math.Round(Math.Pow(num8, 2));

			// list4
			for (int i = 0; i <= 9; i++)
			{
				list4[i] = list3[table2[num8][i]];
			}

			// list5
			for (int i = 0; i <= 9; i++)
			{
				list5[i] = ((seedten.Substring(i, 1)[0]) + list4[i]) % 36;
			}

			// Finally, build the password of the day.
			var len = list5.Length;
			var passwordOfTheDay = new char[len];
			for (var i = 0; i < len; i++)
			{
				passwordOfTheDay[i] = alphanum[list5[i]];
			}

			return new string(passwordOfTheDay);
		}
	}
}
