using System;
using System.Collections.Generic;


public class Validate {
    public Language nl { get; set; }
    public Language en { get; set; }

    public Validate()
    {
        nl = new Language();
        en = new Language();
    }

}

public class Language{
    public string value {get; set;}
    public ValidateObject validate { get; set; }

}

public class ValidateObject{
    public bool allow_changes { get; set; }
    public bool required { get; set; }
    public bool publication_ok { get; set; }
    public Nullable<Int16> validated_by { get; set; }
    public string feedback { get; set; }
    // public Array editable_by { get; set; }
    public string visible_to { get; set; }
    public Nullable<DateTime> updated_at { get; set; }

}

