# FileValidator

![image](https://user-images.githubusercontent.com/51239869/131505693-2837634b-bc50-4451-8181-6e147ad3b596.png)

The goal of this project was to create a simple solution to enable validation of log file.

<br />
Project consists of two parts:

- ```Blazor Server``` app written using MVVM pattern to keep markup and logic neatly separeted
- ```Core Library``` containig logic and predefined rules for line validator

<br />

The assumed file structure consists of N rows (However the total size must be under 3MiB): 
<br />
```Event-Name; Event-Description; Start-Date-Time; End-Date-Time;```
<br />
With constraints:
- ```Event-Name``` is not null or empty and under 32 characters long
- ```Event-Description``` is not null or empty and under 255 characters long
- ```Start-Date``` matches format ```yyyy-MM-ddTHH:mm:sszzz```
- ```End-Date``` matches format ```yyyy-MM-ddTHH:mm:sszzz```


<br />
<br />

To create your own validation rule make use of:
```
new CsvLineParserBuilder()
  .WithSeparator(';')
  .WithProperty(propName, errorMsg, params validationPredicates[])
  .Build();
```

There is some settings made by default (Including max file size, allowed extensions of file and path to temporarily stored file). You may easily change them via modifying ```appsettings.json``` file.
