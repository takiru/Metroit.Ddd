# Metroit.Ddd
Useful instructions for Domain driven design

## 用意された命令
Application.Interfaces.IStorageService  
Application.Interfaces.IUnitOfWork  
Application.Interfaces.Generic.IStorageService  
ContentRoot.DIConfigration  
ContentRoot.DIConfigurationServiceBuilder  
ContentRoot.DiDbContextConfig  
ContentRoot.DiRootConfig  
ContentRoot.DiServiceConfig  
ContentRoot.DIServiceConfigurationBuilder  
ContentRoot.IDIDbContextConfiguration  
ContentRoot.IDILoggerConfiguration  
ContentRoot.IDIServiceConfiguration  
ContentRoot.ServiceCollectionJsonExtensions  
Domain.Annotations.VOEmailAddressAttribute  
Domain.Annotations.VOFeedOrderAttribute  
Domain.Annotations.VOGreaterThanAttribute  
Domain.Annotations.VOLengthAttribute  
Domain.Annotations.VOLessThanAttribute  
Domain.Annotations.VOMaxLengthAttribute  
Domain.Annotations.VOMinLengthAttribute  
Domain.Annotations.VORangeAttribute  
Domain.Annotations.VORegularExpressionAttribute  
Domain.Annotations.VORequiredAttribute  
Domain.Annotations.VOStringLengthAttribute  
Domain.ValueObjects.ISingleValueObject  
Domain.ValueObjects.MultiValueObject  
Domain.ValueObjects.SingleValueObject  
Domain.ValueObjects.ValueObject  
Infrastructure.Services.DirectoryService  
Infrastructure.Services.FileService  
Infrastructure.Services.Generic.DirectoryService    
Infrastructure.Services.Generic.FileService  
Presentation.Extensions.IServiceScopeFactoryExtensions  

## 単一のValueObject
```cs
// 値を受け入れた直後に即座に検証を行います
[Display(Name = "名前")]
[VORequired(ErrorMessage = "{0}は必須です。")]
[VOStringLength(10, ErrorMessage = "{0}は{1}文字までです。")]
public class Name : SingleValueObject<string>
{
    public Name(string value) : base(value)
    {
    }
}

// 任意のタイミングで検証を行います
[Display(Name = "名前")]
[VORequired(ErrorMessage = "{0}は必須です。")]
[VOStringLength(10, ErrorMessage = "{0}は{1}文字までです。")]
public class Name : SingleValueObject<string>
{
    public Name(string value) : base(false, value)
    {
        // 認められない値が見つかった時点で ValidationException が発生します
        ValidateObject();
    }
}

// 任意のタイミングで検証を行い、結果を自由に制御します
[Display(Name = "名前")]
[VORequired(ErrorMessage = "{0}は必須です。")]
[VOStringLength(10, ErrorMessage = "{0}は{1}文字までです。")]
public class Name : SingleValueObject<string>
{
    public Name(string value) : base(false, value)
    {
        ICollection<ValidationResult> r;
        if (!TryValidateObject(out r))
        {
            throw new ArgumentException(r.First().ErrorMessage);
        }
    }
}
```

## 複数のValueObject
```cs
// 値を受け入れた直後に即座に検証を行います
public class Name : MultiValueObject
{
    [Display(Name = "名")]
    [VOFeedOrder(0)]
    [VORequired(ErrorMessage = "{0}は必須です。")]
    [VOStringLength(5, ErrorMessage = "{0}は{1}文字までです。")]
    public string FirstName { get; private set; }

    [Display(Name = "姓")]
    [VOFeedOrder(1)]
    [VORequired(ErrorMessage = "{0}は必須です。")]
    [VOStringLength(5, ErrorMessage = "{0}は{1}文字までです。")]
    public string LastName { get; private set; }

    public Name(string firstName, string lastName) : base(firstName, lastName)
    {
    }
}


// 任意のタイミングで検証を行います
public class Name : MultiValueObject
{
    [Display(Name = "名")]
    [VOFeedOrder(0)]
    [VORequired(ErrorMessage = "{0}は必須です。")]
    [VOStringLength(5, ErrorMessage = "{0}は{1}文字までです。")]
    public string FirstName { get; private set; }

    [Display(Name = "姓")]
    [VOFeedOrder(1)]
    [VORequired(ErrorMessage = "{0}は必須です。")]
    [VOStringLength(5, ErrorMessage = "{0}は{1}文字までです。")]
    public string LastName { get; private set; }

    public Name(string firstName, string lastName) : base(false, firstName, lastName)
    {
        // 認められない値が見つかった時点で ValidationException が発生します
        ValidateObject();
    }
}

// 任意のタイミングで検証を行い、結果を自由に制御します
public class Name : MultiValueObject
{
    [Display(Name = "名")]
    [VOFeedOrder(0)]
    [VORequired(ErrorMessage = "{0}は必須です。")]
    [VOStringLength(5, ErrorMessage = "{0}は{1}文字までです。")]
    public string FirstName { get; private set; }

    [Display(Name = "姓")]
    [VOFeedOrder(1)]
    [VORequired(ErrorMessage = "{0}は必須です。")]
    [VOStringLength(5, ErrorMessage = "{0}は{1}文字までです。")]
    public string LastName { get; private set; }

    public Name(string firstName, string lastName) : base(false, firstName, lastName)
    {
        ICollection<ValidationResult> r;
        if (!TryValidateObject(out r))
        {
            throw new ArgumentException(r.First().ErrorMessage);
        }
    }
}
```


## 従来の Windows Forms アプリケーション
Properties 内に launchSettings.json を用意し、下記のように用意します。
"ProjectName" は、launchSettings.json を配置したプロジェクトの名前です。

```json
{
  "profiles": {
    "ProjectName": {
      "commandName": "Project",
      "environmentVariables": {
        "DOTNET_ENVIRONMENT": "Development"
      }
    }
  }
}
```

さらに appsettings.json および appsettings.development.json をプロジェクト内に用意します。

コードは下記の通り。
```cs
var di = new TestDIConfiguration();
di.Configure();
```

これで、appsettings.development.json の設定値を読み込むことができます。

## DIの登録
DIConfiguration, IDIServiceConfiguration を実装することにより、DIの登録を複数の IDIServiceConfiguration に分けて管理および登録することができます。

```cs
public class SampleDIConfiguration : DIConfigration
{
    protected override void OnServiceConfiguring(DIServiceConfigurationBuilder builder)
    {
      builder.ApplyConfiguration(new InfrastructureServiceConfiguration());
        builder.ApplyConfiguration(new PresentationServiceConfiguration());
    }
}

public class InfrastructureServiceConfiguration : IDIServiceConfiguration
{
    public void Configure(DIConfigurationServiceBuilder builder)
    {
      builder.Services.AddTransient(typeof(Form1));
    }
}

public class PresentationServiceConfiguration : IDIServiceConfiguration
{
    public void Configure(DIConfigurationServiceBuilder builder)
    {
      builder.Services.AddTransient(typeof(Form1));
    }
}
```
```cs
var di = new SampleDIConfiguration();
di.Configure();
```

## 設定情報の読み込み
appsettings.json 以外にも、DIConfigration.JsonStreams および JsonStreams.JsonFiles プロパティを利用することで、複数のJSONデータを追加で読み込むことが可能です。  
これは、[DIの登録](#diの登録) より先に実行されます。

```cs
var di = new SampleDIConfiguration();
di.JsonStreams = new[] { new FileStream("di.json", FileMode.Open, FileAccess.Read, FileShare.Read) };
di.Configure();
```

同一キーの情報は、最後に読み込まれたJSONデータが有効になります。  
ただし、`DependencyInjection` から始まるキー情報に限り、後述する制御が行われます。  

### DIへの登録
JSONデータに下記のように用意すると、自動的にDI登録を行います。

```json
{
  "DependencyInjection": {
    "Loggers": [ "NLog" ],
    "Services": [
      {
        "Interface" : "Test.IMainForm, Test",
        "Implementation": "Test.Form2, Test",
        "Lifetime": "Transient"
      },
      {
        "Implementation": "Test.Form2, Test",
        "Lifetime": "Scoped"
      },
      {
        "Implementation": "Test.Form3, Test",
        "Lifetime": "Transient",
        "UseInstance": true,
        "Args":[ "hoge", 123 ],
        "Settings":[
            {"Property1", "hoge"},
            {"Property2", 123 }
        ]
      }
    ],
    "DbContexts": [
      {
        "ContextName": "YourName",
        "TypeName": "Test.MyDbContext, Test",
        "ConnectionStringName": "DefaultDbContext",
        "Lifetime": "Scoped",
        "Options": {
          "EnableSensitiveDataLogging": true,
          "CommandTimeout": 30
        }
      }
    ]
}
```

ロガーは `Loggers` キーに複数指定可能です。  
サービスは `Services` キーに複数指定可能です。  
  | キー | 意味 |
  |-|-|
  | Interface | インターフェースのタイプ名。 |
  | Implementation | 実装のタイプ名。 |
  | Lifetime | ライフタイム。`Transient`, `Scoped`, `Singleton` のいずれか。 |
  | UseInstance | インスタンスを登録するときは `true`。 |
  | Args | インスタンスのコンストラクタへ渡す引数の値。 |
  | Settings | インスタンスのプロパティ名と値。 |

データベースは `DbContexts` キーに複数指定可能です。
  | キー | 意味 |
  |-|-|
  | ContextName | コンテキスト名。 |
  | TypeName | `DbContext` のタイプ名。 |
  | ConnectionStringName | appsettings.json などに含まれる `ConnectionStrings` キー内の接続文字列を有しているキー名。 |
  | Lifetime | ライフタイム。`Transient`, `Scoped`, `Singleton` のいずれか。 |
  | Options | `EnableSensitiveDataLogging`, `CommandTimeout` を指定可能。 |

`DependencyInjection` から始まるキー名は、すべてDIの設定情報とみなします。  
そのため、複数ファイルにまたがってDIの設定情報を定義したいとき、`DependencyInjection`, `DependencyInjection001`, `DependencyInjection002` などとキーを用意しておくとすべてDIの設定情報をみなします。  
よって、JSONデータは上書きを行わないため、同じロガーやサービスなどを重複して登録しないよう注意が必要です。  

ロガーやデータベースを利用するとき、DIConfigration.LoggerConfigurations, DIConfigration.DbContextConfigurations の設定が必要です。  

```cs
var di = new SampleDIConfiguration();
di.LoggerConfigurations = new() { { "NLog", new DINLogConfiguration() } };
di.DbContextConfigurations = new() { { "YourName", new DIMsSqlConfiguration() } };
di.Configure();
```

## プレゼンテーション層からのサービス取得と実行
クライアントアプリケーションでは、DIサービスのライフタイムをコマンドごとにしたいとき、下記を満たさなければなりません。
 - DIサービスのライフタイムが `Scoped` である。
 - コマンドを実行する度に IServiceScopeFactory.CreateScope() の実行が必要。
 - そのため、サービスの実行を必要とするクラス（主に ViewModel を想定）では、IServiceScopeFactory をDIする必要がある。

 コマンドごとのスコープの管理として IServiceScopeFactory.CreateScope() を正しく使用することが少し煩雑になるため、ExecuteInScope(), ExecuteInScopeAsync() を利用することで簡略化することができます。

```cs
public class SampleViewModel
{
    private readonly IServiceScopeFactory ServiceScopeFactory;

    public SampleViewModel(IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
    }

    public async Task TestMethod()
    {
        await ServiceScopeFactory.ExecuteInScopeAsync<TestAppService>(async (serviceProvider, service) =>
        {
            var cnt = await service.Register();
            Debug.WriteLine($"登録件数：{cnt}");
        });
    }
}
```

複数のサービスが必要なときは、`serviceProvider.GetRequiredService<T>()` を利用することで同一スコープ内でサービスの取得ができますが、ユースケースで必要なサービスはファサードクラスなどでひとまとめにしておくべきでしょう。  

依存インターフェース/クラスが内部に隠れてしまうのは、クライアントアプリケーションでは仕方なしです。  
※ 画面が起動している間、ずっと同じオブジェクトでいいなら別ですが、なんだかんだDbContext絡みとかの関係上、個人的には嫌がりました。  


# Metroit.Ddd.EntityFrameworkCore

## 用意された命令
EFRepositoryBase  
EFServiceBase  
EFUnitOfWork  

EFRepositoryBase にある下記メソッドは、呼出し後に必ず SaveChanges() またはSaveChangesAsync() が実行されます。  
さらに、InstantlyClearChangeTracker プロパティを `true` にしたとき、すべての ChangeTracker をクリアします。  
 - Add()
 - AddAsync()
 - AddRange()
 - AddRangeAsync()
 - Update()
 - UpdateAsync()
 - UpdateRange()
 - UpdateRangeAsync()
 - Remove()
 - RemoveAsync()
 - RemoveRange()
 - RemoveRangeAsync()

# Metroit.Ddd.EntityFrameworkCore.SqlServer

## 用意された命令
DIMsSqlConfiguration

[DIへの登録](#diへの登録) で利用されます。

# Metroit.Ddd.NLog

## 用意された命令
DINLogConfiguration

[DIへの登録](#diへの登録) で利用されます。
