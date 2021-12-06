using Newtonsoft.Json;

var items = JsonConvert.DeserializeObject<int[]>("[1,2,3,4,5]");
var list = ListNode.Create(items);
var solution = new Solution();

var result = solution.OddEvenList(list);
var resultArray = ListNode.ToArray(result);

Console.WriteLine(JsonConvert.SerializeObject(resultArray));