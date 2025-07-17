# Metroit.Ddd
Useful instructions for Domain driven design

## 用意された命令
Application.Interfaces.IStorageService  
Application.Interfaces.IUnitOfWork  
Application.Interfaces.Generic.IStorageService  
ContentRoot.DIConfigration  
ContentRoot.DIConfigurationBuilder  
ContentRoot.DIConfigurationServiceBuilder  
ContentRoot.IDITypeConfiguration  
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

さらに appsettings.json および appsettings.json.development.json をプロジェクト内に用意します。

コードは下記の通り。
```cs
var di = new TestDIConfiguration();
di.Configure();
```

これで、appsettings.json.development.json の設定値を読み込むことができます。

# Metroit.Ddd.EntityFrameworkCore

## 用意された命令
EFRepositoryBase  
EFServiceBase  
EFUnitOfWork  
