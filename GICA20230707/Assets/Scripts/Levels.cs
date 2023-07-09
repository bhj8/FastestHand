using System.Collections.Generic;

public class Levels 
{
    private static Levels instance = null;
    private static int nowLevelIndex = 0;

    //按钮类型 1 普通 2 按两下 3 红色不能按 4 外框长按 5 虚拟拖拽
    private static List<int> level1 = new List<int> { 1,1,1,2,2,2,3,3,3 };
    private static List<int> level2 = new List<int> { 1,1,1,2,2,2,3,3,3 };
    private static List<int> level3 = new List<int> { 1,1,1,2,2,2,3,3,3 };
    private static List<int> level4 = new List<int> { 1,1,1,2,2,2,3,3,3 };

    private static List<List<int>> allLevel = new List<List<int>> { level1, level2, level3, level4 };

    // 私有构造方法
    private Levels() { }

    // 单例的访问方法
    public static Levels Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Levels();
            }
            return instance;
        }
    }

    public static List<int> GetLevelData()
    {
        return allLevel[nowLevelIndex];
    }

    public static void IncrementLevel()
    {
        nowLevelIndex = (nowLevelIndex + 1) % allLevel.Count;
    }
}
