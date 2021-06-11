# Online Music Store
---
Mini Project on ASP.NET Core MVC.

---

### Directory Structure
```
root // Repository root
   |
   |_Design // for all the designs and software documents
   |
   |_Database // for .mdf and .ldf files
   |
   |_Demo Assets backup // for demo image files
   |
   |_Implementation // Contains the Project root folder.
   | |
   | |_OnlineMusicStore // Project root folder
   |
   |_LICENSE // License file
   |
   |_README.md // this file.
```

### Setup

1. Clone the repo.
2. Change working directory to the Project root folder.
3. Issue command ```dotnet restore``` to install all the dependencies.
4. You are good to go. Edit or run as you wish.

_Commands for setup_

```
git clone https://github.com/the-it-weirdo/OnlineMusicStore.git
cd OnlineMusicStore/Implementation/OnlineMusicStore
dotnet restore # optional. If 'dotnet run' fails, use this command.
dotnet run
```

_Setting up the database for 1st time_
 - Install the ef cli for .net core
```
dotnet tool install --global dotnet-ef
```
 - Apply Migrations
 ```
 dotnet ef database update
 ```

 _Getting the data from pre created database_
  1. The Database dir has a .mdf and a .ldf file.
  2. After you apply the migrations, similar files will be created in your local machine (For windows: ```C:\Users\<current_user>```)
  3. Note the filename of the .mdf and .ldf file for this project from ```C:\Users\<current_user>```
  4. Rename the files in the Database dir of this repo, to the names collected in 3.
  5. Replace the files at ```C:\Users\<current_user>``` with the files from the Database dir.

**N.B.: If you want to edit, please use ```vscode .``` inplace of ```dotnet run``` as the last command.**

---
### Collaborators
- [Ayushi Bhowmik](https://github.com/ayushibhowmik)
- [Debaleen Das Spandan](https://github.com/the-it-weirdo)
- [Muskan Singhal](https://github.com/muskan3218)
- [Akshat Kothiyal](https://github.com/KothiyalAkshat)

### License
```
MIT License

Copyright (c) 2021 Debaleen Das Spandan, Muskan Singhal, Ayushi Bhowmik, Akshat Kothiyal

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
