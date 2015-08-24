
if (!Date.prototype.addSecondes)
{
    Date.prototype.addSecondes = function (x)
    {
        var diff = x * 1000;
        return new Date(this.valueOf() + diff);
    };
}

if (!Date.prototype.addMinutes)
{
    Date.prototype.addMinutes = function (x)
    {
        var diff = x * 1000 * 60;
        return new Date(this.valueOf() + diff);
    };
}

if (!Date.prototype.addHours)
{
    Date.prototype.addHours = function (x)
    {
        var diff = x * 1000 * 60 * 60;
        return new Date(this.valueOf() + diff);
    };
}

if (!Date.prototype.addDays)
{
    Date.prototype.addDays = function (x)
    {
        var diff = x * 1000 * 60 * 60 * 24;
        return new Date(this.valueOf() + diff);
    };
}

if (!Date.prototype.addMonths)
{
    Date.prototype.addMonths = function (value)
    {
        var n = this.getDate();
        this.setDate(1);
        this.setMonth(this.getMonth() + value);
        this.setDate(Math.min(n, this.getDaysInMonth()));
        return this;
    };
}

if (!Date.prototype.addYears)
{
    Date.prototype.addYears = function (value)
    {
        this.setFullYear(this.getFullYear() + value);
        return this;
    };
}
if (!Date.prototype.getFullYear)
{
    Date.prototype.getFullYear = function () { var yy = this.getYear(); return (yy < 1900 ? yy + 1900 : yy); };
}

if (!Date.prototype.getDaysInMonth)
{
    Date.prototype.getDaysInMonth = function (year, month)
    {
        if (arguments.length == 0)
        {
            var d = new Date();
            year = d.getFullYear();
            month = d.getMonth() + 1;
        }
        return [31, (Date.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
    };
}

if (!Date.prototype.isLeapYear)
{
    Date.isLeapYear = function (year)
    {
        return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0));
    };

    Date.prototype.isLeapYear = function ()
    {
        var y = this.getFullYear();
        return (((y % 4 === 0) && (y % 100 !== 0)) || (y % 400 === 0));
    };
}

if (!Date.prototype.StartOFDay)
{
    Date.prototype.StartOFDay = function ()
    {
        return new Date(this.getFullYear(), this.getMonth(), this.getDate(), 0, 0, 0, 0);
    }
}

if (!Date.prototype.StartOFWeek)
{
    Date.prototype.StartOFWeek = function (deafultStartDay)
    {
        if (arguments.length == 0)
        {
            deafultStartDay = 0;
        }
        var diff = this.getDay() - deafultStartDay;

        if (diff < 0)
        {
            diff += 7;
        }

        return this.addDays(-1 * diff);
    }
}

if (!Date.prototype.StartOfMonth)
{
    Date.prototype.StartOfMonth = function ()
    {
        return new Date(this.getFullYear(), this.getMonth(), 1, 0, 0, 0, 0)
    }
}

if (!Date.prototype.StartOfYear)
{
    Date.prototype.StartOfYear = function ()
    {
        return new Date(this.getFullYear(), 0, 1, 0, 0, 0, 0)
    }
}

if (!Date.prototype.StartOfQuarter)
{
    Date.prototype.StartOfQuarter = function ()
    {
        var Quarter = Math.floor(this.getMonth() / 3) + 1;
        var month = 0;
        switch (Quarter)
        {
            case 1: month = 1; break;
            case 2: month = 3; break;
            case 3: month = 6; break;
            case 4: month = 9; break;
        }
        return new Date(this.getFullYear(), month, 1, 0, 0, 0, 0)
    }
}

if (!Date.prototype.EndOFDay)
{
    Date.prototype.EndOFDay = function ()
    {
        return new Date(this.getFullYear(), this.getMonth(), this.getDate(), 0, 0, 0, 0);
    }
}

if (!Date.prototype.EndOfWeek)
{
    Date.prototype.EndOfWeek = function (deafultStartDay)
    {
        deafultStartDay = 6;
        var d = this;

        while (d.getDay() != deafultStartDay)
        {
            d = d.addDays(1);
        }

        return d;
    }
}

if (!Date.prototype.EndOfMonth)
{
    Date.prototype.EndOfMonth = function ()
    {
        return new Date(this.getFullYear(), this.getMonth(), this.getDaysInMonth(), 23, 59, 0, 0)
    }
}

if (!Date.prototype.EndOfYear)
{
    Date.prototype.EndOfYear = function ()
    {
        return new Date(this.getFullYear(), 12, 31, 0, 0, 0, 0)
    }
}

if (!Date.prototype.EndOfQuarter)
{
    Date.prototype.EndOfQuarter = function ()
    {
        var d = this.StartOfQuarter();
        return d.addMonths(3).addDays(-1);
    }
}

Date.prototype.isBefore = function (date2)
{
    if (date2 == null)
    {
        return false;
    }
    return (this.getTime() < date2.getTime());
};

Date.prototype.isAfter = function (date2)
{
    if (date2 == null)
    {
        return false;
    }
    return (this.getTime() > date2.getTime());
};

Date.prototype.equals = function (date2)
{
    if (date2 == null)
    {
        return false;
    }
    return (this.getTime() == date2.getTime());
};

// Check if two date objects have equal dates, disregarding times
Date.prototype.IFDateEquals = function (date2)
{
    if (date2 == null)
    {
        return false;
    }
    var d1 = new Date(this.getTime()).StartOFDay();
    var d2 = new Date(date2.getTime()).StartOFDay();
    return (d1.getTime() == d2.getTime());
};

Date.LZ = function (x) { return (x < 0 || x > 9 ? "" : "0") + x };

Date.monthNames = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');

Date.monthAbbreviations = new Array('Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec');

Date.dayNames = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');

Date.dayAbbreviations = new Array('Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat');

Date.prototype.getDayName = function ()
{
    return Date.dayNames[this.getDay()];
};

Date.prototype.getMonthName = function ()
{
    return Date.monthNames[this.getMonth()];
};
// Format a date into a string using a given format string
Date.prototype.format = function (format)
{
    format = format + "";
    var result = "";
    var i_format = 0;
    var c = "";
    var token = "";
    var y = this.getYear() + "";
    var M = this.getMonth() + 1;
    var d = this.getDate();
    var E = this.getDay();
    var H = this.getHours();
    var m = this.getMinutes();
    var s = this.getSeconds();
    var yyyy, yy, MMM, MM, dd, hh, h, mm, ss, ampm, HH, H, KK, K, kk, k;
    // Convert real date parts into formatted versions
    var value = new Object();
    if (y.length < 4)
    {
        y = "" + (+y + 1900);
    }
    value["y"] = "" + y;
    value["yyyy"] = y;
    value["yy"] = y.substring(2, 4);
    value["M"] = M;
    value["MM"] = Date.LZ(M);
    value["MMM"] = Date.monthNames[M - 1];
    value["NNN"] = Date.monthAbbreviations[M - 1];
    value["d"] = d;
    value["dd"] = Date.LZ(d);
    value["E"] = Date.dayAbbreviations[E];
    value["EE"] = Date.dayNames[E];
    value["H"] = H;
    value["HH"] = Date.LZ(H);
    if (H == 0)
    {
        value["h"] = 12;
    }
    else if (H > 12)
    {
        value["h"] = H - 12;
    }
    else
    {
        value["h"] = H;
    }
    value["hh"] = Date.LZ(value["h"]);
    value["K"] = value["h"] - 1;
    value["k"] = value["H"] + 1;
    value["KK"] = Date.LZ(value["K"]);
    value["kk"] = Date.LZ(value["k"]);
    if (H > 11)
    {
        value["a"] = "PM";
    }
    else
    {
        value["a"] = "AM";
    }
    value["m"] = m;
    value["mm"] = Date.LZ(m);
    value["s"] = s;
    value["ss"] = Date.LZ(s);
    while (i_format < format.length)
    {
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length))
        {
            token += format.charAt(i_format++);
        }
        if (typeof (value[token]) != "undefined")
        {
            result = result + value[token];
        }
        else
        {
            result = result + token;
        }
    }
    return result;
};

String.prototype.DateParse = function ( format)
{
    var val = this;
    /// <summary>parse string to Date ((new Date).parseString("07/08/2014 05:20",'MM/dd/yyyy hh:mm' ))</summary>
    // If no format is specified, try a few common formats
    // Used for parsing ambiguous dates like 1/2/2000 - default to preferring 'American' format meaning Jan 2.
    // Set to false to prefer 'European' format meaning Feb 1
    Date.preferAmericanFormat = true;
    if (typeof (format) == "undefined" || format == null || format == "")
    {
        var generalFormats = new Array('y-M-d', 'MMM d, y', 'MMM d,y', 'y-MMM-d', 'd-MMM-y', 'MMM d', 'MMM-d', 'd-MMM');
        var monthFirst = new Array('M/d/y', 'M-d-y', 'M.d.y', 'M/d', 'M-d');
        var dateFirst = new Array('d/M/y', 'd-M-y', 'd.M.y', 'd/M', 'd-M');
        var checkList = new Array(generalFormats, Date.preferAmericanFormat ? monthFirst : dateFirst, Date.preferAmericanFormat ? dateFirst : monthFirst);
        for (var i = 0; i < checkList.length; i++)
        {
            var l = checkList[i];
            for (var j = 0; j < l.length; j++)
            {
                var d = Date.parseString(val, l[j]);
                if (d != null)
                {
                    return d;
                }
            }
        }
        return null;
    };

    this.isInteger = function (val)
    {
        for (var i = 0; i < val.length; i++)
        {
            if ("1234567890".indexOf(val.charAt(i)) == -1)
            {
                return false;
            }
        }
        return true;
    };
    this.getInt = function (str, i, minlength, maxlength)
    {
        for (var x = maxlength; x >= minlength; x--)
        {
            var token = str.substring(i, i + x);
            if (token.length < minlength)
            {
                return null;
            }
            if (this.isInteger(token))
            {
                return token;
            }
        }
        return null;
    };
    val = val + "";
    format = format + "";
    var i_val = 0;
    var i_format = 0;
    var c = "";
    var token = "";
    var token2 = "";
    var x, y;
    var year = new Date().getFullYear();
    var month = 1;
    var date = 1;
    var hh = 0;
    var mm = 0;
    var ss = 0;
    var ampm = "";
    while (i_format < format.length)
    {
        // Get next token from format string
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length))
        {
            token += format.charAt(i_format++);
        }
        // Extract contents of value based on format token
        if (token == "yyyy" || token == "yy" || token == "y")
        {
            if (token == "yyyy")
            {
                x = 4; y = 4;
            }
            if (token == "yy")
            {
                x = 2; y = 2;
            }
            if (token == "y")
            {
                x = 2; y = 4;
            }
            year = this.getInt(val, i_val, x, y);
            if (year == null)
            {
                return null;
            }
            i_val += year.length;
            if (year.length == 2)
            {
                if (year > 70)
                {
                    year = 1900 + (year - 0);
                }
                else
                {
                    year = 2000 + (year - 0);
                }
            }
        }
        else if (token == "MMM" || token == "NNN")
        {
            month = 0;
            var names = (token == "MMM" ? (Date.monthNames.concat(Date.monthAbbreviations)) : Date.monthAbbreviations);
            for (var i = 0; i < names.length; i++)
            {
                var month_name = names[i];
                if (val.substring(i_val, i_val + month_name.length).toLowerCase() == month_name.toLowerCase())
                {
                    month = (i % 12) + 1;
                    i_val += month_name.length;
                    break;
                }
            }
            if ((month < 1) || (month > 12))
            {
                return null;
            }
        }
        else if (token == "EE" || token == "E")
        {
            var names = (token == "EE" ? Date.dayNames : Date.dayAbbreviations);
            for (var i = 0; i < names.length; i++)
            {
                var day_name = names[i];
                if (val.substring(i_val, i_val + day_name.length).toLowerCase() == day_name.toLowerCase())
                {
                    i_val += day_name.length;
                    break;
                }
            }
        }
        else if (token == "MM" || token == "M")
        {
            month = this.getInt(val, i_val, token.length, 2);
            if (month == null || (month < 1) || (month > 12))
            {
                return null;
            }
            i_val += month.length;
        }
        else if (token == "dd" || token == "d")
        {
            date = this.getInt(val, i_val, token.length, 2);
            if (date == null || (date < 1) || (date > 31))
            {
                return null;
            }
            i_val += date.length;
        }
        else if (token == "hh" || token == "h")
        {
            hh = this.getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 12))
            {
                return null;
            }
            i_val += hh.length;
        }
        else if (token == "HH" || token == "H")
        {
            hh = this.getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 23))
            {
                return null;
            }
            i_val += hh.length;
        }
        else if (token == "KK" || token == "K")
        {
            hh = this.getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 11))
            {
                return null;
            }
            i_val += hh.length;
            hh++;
        }
        else if (token == "kk" || token == "k")
        {
            hh = this.getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 24))
            {
                return null;
            }
            i_val += hh.length;
            hh--;
        }
        else if (token == "mm" || token == "m")
        {
            mm = this.getInt(val, i_val, token.length, 2);
            if (mm == null || (mm < 0) || (mm > 59))
            {
                return null;
            }
            i_val += mm.length;
        }
        else if (token == "ss" || token == "s")
        {
            ss = this.getInt(val, i_val, token.length, 2);
            if (ss == null || (ss < 0) || (ss > 59))
            {
                return null;
            }
            i_val += ss.length;
        }
        else if (token == "a")
        {
            if (val.substring(i_val, i_val + 2).toLowerCase() == "am")
            {
                ampm = "AM";
            }
            else if (val.substring(i_val, i_val + 2).toLowerCase() == "pm")
            {
                ampm = "PM";
            }
            else
            {
                return null;
            }
            i_val += 2;
        }
        else
        {
            if (val.substring(i_val, i_val + token.length) != token)
            {
                return null;
            }
            else
            {
                i_val += token.length;
            }
        }
    }
    // If there are any trailing characters left in the value, it doesn't match
    if (i_val != val.length)
    {
        return null;
    }
    // Is date valid for month?
    if (month == 2)
    {
        // Check for leap year
        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
        { // leap year
            if (date > 29)
            {
                return null;
            }
        }
        else
        {
            if (date > 28)
            {
                return null;
            }
        }
    }
    if ((month == 4) || (month == 6) || (month == 9) || (month == 11))
    {
        if (date > 30)
        {
            return null;
        }
    }
    // Correct hours value
    if (hh < 12 && ampm == "PM")
    {
        hh = hh - 0 + 12;
    }
    else if (hh > 11 && ampm == "AM")
    {
        hh -= 12;
    }
    return new Date(year, month - 1, date, hh, mm, ss);
};



