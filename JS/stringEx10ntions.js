
if (!String.prototype.toInt)
{
    String.prototype.toInt = function ()
    {
        return parseInt(this, 10);
    }
}

if (!String.prototype.toFloat)
{
    String.prototype.toFloat = function ()
    {
        return parseFloat(this, 10);
    }
}

if (!String.prototype.toBool)
{
    String.prototype.toBool = function ()
    {
        return this == "true" || this == "1";
    }
}


if (!String.prototype.SubstringBefore)
{
    String.prototype.SubstringBefore = function (suffix)
    {
        var str = this;
        return (str.substring(0, str.indexOf(suffix) + suffix.length));
    }
}

if (!String.prototype.SubstringAfter)
{
    String.prototype.SubstringAfter = function (preffix)
    {
        var str = this;
        return (str.substring(str.indexOf(preffix) + preffix.length, str.length));
    }
}

if (!String.prototype.insert)
{
    String.prototype.insert = function (index, txt)
    {
        var str = this;
        return str.substr(0, index) + txt + str.substr(index);
    }
}

if (!String.prototype.format)
{
    String.prototype.format = function ()
    {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number)
        {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

if (!String.prototype.EndsWith)
{
    String.prototype.EndsWith = function (suffix)
    {
        var str = this;
        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }
}

if (String.prototype.startsWith)
{
    String.prototype.startsWith = function (preffix)
    {
        return this.indexOf(preffix) == 0;
    };
}

if (!String.prototype.replaceAll)
{
    String.prototype.replaceAll = function (oldString, newString)
    {
        return this.split(oldString).join(newString);
    };
}

if (!String.prototype.endsWith)
{
    String.prototype.endsWith = function (suffix)
    {
        var str = this;
        return str.indexOf(suffix, str.length - suffix.length) !== -1;
    }
}

if (!String.prototype.trim)
{
    String.prototype.trim = function ()
    {
        var str = this;
        return str.replace(/^\s+|\s+$/gm, '');
    }
}

if (!String.prototype.contains)
{
    String.prototype.contains = function (str)
    {
        return this.indexOf(str) != -1;
    }
}

if (!String.prototype.reverse)
{
    String.prototype.reverse = function ()
    {
        var s = "";
        var i = this.length;
        while (i > 0)
        {
            s += this.substring(i - 1, i);
            i--;
        }
        return s;
    }
}

if(!String.prototype.splitWithStringSplitOptions)
{
	
	String.prototype.splitWithStringSplitOptions = function (splitBy, removeItemString)
	{ 
		if (this == "") 
		{ 
			return new Array(); 
		} 
		
		var items = this.split(splitBy);

		for (var i = 0; i < items.length; i++) 
		{ 
			if (items[i] == removeItemString) 
			{ 
				items.splice(i, 1); 
				i--; 
			} 
		} 
		return items; 
	}
}

