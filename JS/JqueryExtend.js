jQuery.fn.extend({
    check: function ()
    {
        return this.each(function ()
        {
            this.checked = true;
        });
    },
    uncheck: function ()
    {
        return this.each(function ()
        {
            this.checked = false;
        });
    },
    disable: function ()
    {
        return this.each(function ()
        {
            $(this).attr("disabled", true);
        });
    },
    enable: function ()
    {
        return this.each(function ()
        {
            $(this).removeAttr("disabled");
        });
    },

    isChecked: function ()
    {
        return $(this).filter(":checked");
    },
    visible: function ()
    {
        return this.each(function ()
        {
            $(this).css('visibility', 'visible');
        });
    },
    invisible: function ()
    {
        return this.each(function ()
        {
            $(this).css('visibility', 'hidden');
        });
    },
    top: function (number)
    {
        return this.each(function ()
        {
            $(this).css('top', number);
        });
    },
    left: function (number)
    {
        return this.each(function ()
        {
            $(this).css('left', number);
        });
    },
    changeTop: function (number)
    {
        return this.each(function ()
        {
            var old = $(this).css('top');
            if (old == "auto")
            {
                $(this).css('top', number);
            }
            else
            {
                old = cleanNumber(old);
                $(this).css('top', old + number);
            }

        });
    },
    changeLeft: function (number)
    {
        return this.each(function ()
        {
            var old = $(this).css('left');
            $(this).css('left', old + number);
        });
    },
    src: function (href)
    {
        if (href)
        {
            return this.each(function ()
            {
                $(this).attr('src', href);
            });
        }
        else
        {
            return $(this).attr('src');
        }      
    }
});

function cleanNumber(number)
{
    var noText = number.replace("px", "");
    return parseInt(noText);
}