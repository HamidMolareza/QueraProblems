using System;
using System.Collections.Generic;
using System.Linq;

namespace Quera {
    public static class Program {
        private const int MaximumGroupMember = 3;

        public static void Main() {
            var _ = Console.ReadLine(); //Ignore
            var numOfGroupMembers = GetInputs<int>(' ');

            foreach (var numOfGroupMember in numOfGroupMembers) {
                Console.WriteLine(new string('*',
                    numOfGroupMember <= MaximumGroupMember ? numOfGroupMember : 1));
            }
        }

        private static List<T> GetInputs<T>(char separator) =>
            Console.ReadLine()
                ?.Trim()
                .Split(separator)
                .Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
    }
}