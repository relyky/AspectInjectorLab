# AspectInjector Lab
[Aspect Injector](https://github.com/pamidur/aspect-injector)，一套不輸給 SheepAspect 的AOP方案。

* 有支援 .NET5
* 基底是 [FluentIL](https://github.com/FluentIL/FluentIL) 

# 專案：ConsoleApp1 
目的：可行性測試。

## 結果畫面：正常狀況下執行順序

![正常狀況下執行順序](https://github.com/relyky/AspectInjectorLab/blob/main/doc/Trace%20normally.png)

## 結果畫面：例外狀況下執行順序

![例外狀況下執行順序](https://github.com/relyky/AspectInjectorLab/blob/main/doc/Trace%20exceptionally.png)


# 專案：CatchAndLog 
目的：試作 CatchAndLog，其中透過 ILogger 介面送出Log。

## 結果畫面：CacthAndLog with ILogger
將 CatchAndLog 套用到多個對象看是否正常。

![CacthAndLog with ILogger](https://github.com/relyky/AspectInjectorLab/blob/main/doc/CatchAndLog%20with%20ILogger.png)

## 結果畫面：Async Function
試看看 Async Function 是否也能正常。

![非同步狀況也能正常運轉](https://github.com/relyky/AspectInjectorLab/blob/main/doc/Async%20proceed.png)
