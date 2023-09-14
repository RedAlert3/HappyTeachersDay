using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HappyTeachersHoliday;

internal class Data
{
    internal static List<ClassModel> Classes = new()
    {
#if DEBUG
        new()
        {
            Name = "Test",
            Teacher = "X",
            Begin = DateTime.Parse("2023-09-08 22:37:30"),
            End = DateTime.Parse("2023-09-09 17:50:00")
        },
#else
        // 星期六放
        new()
        {
            Name = "英语",
            Teacher = "贺老师",
            Begin = DateTime.Parse("2023-09-09 08:10:00"),
            End = DateTime.Parse("2023-09-09 08:55:00")
        },
        new()
        {
            Name = "物理",
            Teacher = "罗老师",
            Begin = DateTime.Parse("2023-09-09 09:10:00"),
            End = DateTime.Parse("2023-09-09 09:55:00")
        },
        // 搞活动老师不来
        // 语文
        // 化学
        // 放假下周一
        // 生物
        // 数学
        
        // 星期一放
        new()
        {
            Name = "语文",
            Teacher = "彭老师",
            Begin = DateTime.Parse("2023-09-11 08:10:00"),
            End = DateTime.Parse("2023-09-11 08:55:00")
        },
        new()
        {
            Name = "化学",
            Teacher = "王老师",
            Begin = DateTime.Parse("2023-09-11 09:05:00"),
            End = DateTime.Parse("2023-09-11 09:50:00")
        },
        new()
        {
            Name = "生物",
            Teacher = "杨老师",
            Begin = DateTime.Parse("2023-09-11 11:15:00"),
            End = DateTime.Parse("2023-09-11 12:00:00")
        },
        new()
        {
            Name = "数学",
            Teacher = "禹老师",
            Begin = DateTime.Parse("2023-09-11 14:30:00"),
            End = DateTime.Parse("2023-09-11 15:15:00")
        },

        new()
        {
            Name = "语文",
            Teacher = "彭老师",
            Begin = DateTime.Parse("2023-09-13 10:20:00"),
            End = DateTime.Parse("2023-09-13 11:05:00")
        },
        new()
        {
            Name = "化学",
            Teacher = "王老师",
            Begin = DateTime.Parse("2023-09-12 11:15:00"),
            End = DateTime.Parse("2023-09-12 12:00:00")
        },
        new()
        {
            Name = "语文",
            Teacher = "彭老师",
            Begin = DateTime.Parse("2023-09-13 10:20:00"),
            End = DateTime.Parse("2023-09-13 11:05:00")
        },
#endif
    };

    internal static void Save()
    {
        File.WriteAllText("./data.json", JsonSerializer.Serialize(Classes));
    }

    internal static void Load()
    {
        if (File.Exists(Path.GetFullPath("./data.json")))
        {
            var classes = JsonSerializer.Deserialize<List<ClassModel>>(
                File.ReadAllText("./data.json")
            );
            if (classes is not null) Classes = classes;
        }
    }

    public class ClassModel
    {
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public string? Name { get; set; }

        public string? Teacher { get; set; }

        public bool Activated { get; set; } = false;
    }

}
