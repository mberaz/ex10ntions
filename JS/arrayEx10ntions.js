if (!Array.prototype.select)
{

    Array.prototype.select = function (fnPredicate, args)
    {/// <summary>Description</summary>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        var vals = [];
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var value = fnPredicate.call(this[i]);
                vals.push(value);

            }
        }

        return vals;
    };
}

if (!Array.indexOf)
{
    Array.prototype.indexOf = function (obj, start)
    { /// <summary>return the index of an object the array, if object is not fount -1 is returned</summary>
        /// <param name="obj" type="obj">the object we are looking for</param>
        ///<param name="start" type="number">from were to start looking</param>     
        for (var i = (start || 0) ; i < this.length; i++)
        {
            if (this[i] == obj)
            {
                return i;
            }
        }
        return -1;
    };
}

Array.prototype.clone = function ()
{
    /// <summary>clones a function</summary>    
    var newArray = [];
    for (var i = 0; i < this.length; i++)
    {
        newArray.push(this[i]);
    }
    return newArray;
};

//var items = self.items().where(function() {return this.ItemID() < currentItemID;}); 
if (!Array.prototype.where)
{
    Array.prototype.where = function (fnPredicate, args)
    {/// <summary>return all element from the array that match the filter </summary>
        /// <param name="fnPredicate" type="function">search function delegete (items = self.items().where(function() {return this.ItemID() < currentItemID;}))</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
        {
            throw new TypeError();
        }
        var matches = [];
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var match = fnPredicate.call(this[i]);
                if (match)
                    matches.push(this[i]);
            }
        }

        return matches;
    };
}

//roles.singleOrDefault(function () {return this.RoleId == item.RoleId; }); 
if (!Array.prototype.singleOrDefault)
{
    Array.prototype.singleOrDefault = function (fnPredicate, args)
    {/// <summary>return an obeject from the array that matches the serach, if no object is found null is returned, if more then 1 element is found exception in thrown </summary>
        /// <param name="fnPredicate" type="function">search function delegete (roles.singleOrDefault(function () {return this.RoleId == item.RoleId; }))</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        var matches = [];
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var match = fnPredicate.call(this[i]);
                if (match)
                    matches.push(this[i]);
            }
        }

        if (matches.length > 1)
        {
            throw "Sequence contains more than one matching items.";
        }

        return matches.length == 0 ? null : matches[0];
    };
}
//roles.firstOrDefault(function () {return this.RoleId == item.RoleId; }); 
if (!Array.prototype.firstOrDefault)
{
    Array.prototype.firstOrDefault = function (fnPredicate, args)
    {/// <summary>return an obeject from the array that matches the serach, if no object is found null is returned, if more then 1 element is found the first is returned </summary>
        /// <param name="fnPredicate" type="function">search function delegete (roles.firstOrDefault(function () {return this.RoleId == item.RoleId; }))</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var match = fnPredicate.call(this[i]);
                if (match)
                    return this[i];
            }
        }

        return null;
    };
}

if (!Array.prototype.lastOrDefault)
{
    Array.prototype.lastOrDefault = function (fnPredicate)
    {/// <summary>return an obeject from the array that matches the serach, if no object is found null is returned, if more then 1 element is found the first is returned </summary>
        /// <param name="fnPredicate" type="function">search function delegete (roles.firstOrDefault(function () {return this.RoleId == item.RoleId; }))</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        for (var i = len - 1; i > 0; i--)
        {
            if (i in this)
            {
                var match = fnPredicate.call(this[i]);
                if (match)
                    return this[i];
            }
        }

        return null;
    };
}

//items().max(function() { return  this.ItemID(); }) 
if (!Array.prototype.max)
{
    Array.prototype.max = function (fnValueProvider, args)
    {/// <summary>find the max value for a param in all the array  </summary>
        /// <param name="fnPredicate" type="function">search function delegete ( items().max(function() { return  this.ItemID(); }) )</param>
        var len = this.length;
        if (typeof fnValueProvider != "function")
            throw new TypeError();

        if (len == 0)
            return null;

        var maxItem = this[0];
        var maxValue = fnValueProvider.call(maxItem);
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var val = fnValueProvider.call(this[i]);
                if (maxItem == null || val > maxValue)
                {
                    maxItem = this[i];
                    maxValue = val;
                }
            }
        }

        return maxValue;
    };
}
//items().min(function() { return  this.ItemID(); }) 
if (!Array.prototype.min)
{
    Array.prototype.min = function (fnValueProvider, args)
    {/// <summary>find the min value for a param in all the array  </summary>
        /// <param name="fnPredicate" type="function">search function delegete ( items().min(function() { return  this.ItemID(); }) )</param>
        var len = this.length;
        if (typeof fnValueProvider != "function")
            throw new TypeError();

        if (len == 0)
            return null;

        var minItem = this[0];
        var minValue = fnValueProvider.call(minItem);
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var val = fnValueProvider.call(this[i]);
                if (minItem == null || val < minValue)
                {
                    minItem = this[i];
                    minValue = val;
                }
            }
        }

        return minValue;
    };
}
//items().sum(function() { return  this.Price(); }) 
if (!Array.prototype.sum)
{
    Array.prototype.sum = function (fnValueProvider, args)
    {/// <summary>adds all the values of a param in all the array </summary>
        /// <param name="fnPredicate" type="function">search function delegete  ( items().sum(function() { return  this.Price(); }) )</param>
        var len = this.length;
        if (typeof fnValueProvider != "function")
            throw new TypeError();

        if (len == 0)
            return 0;

        var sum = 0;
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                sum += fnValueProvider.call(this[i]);
            }
        }

        return sum;
    };
}

if (!Array.prototype.average)
{
    Array.prototype.average = function (fnValueProvider)
    {/// <summary>adds all the values of a param in all the array </summary>
        /// <param name="fnPredicate" type="function">search function delegete  ( items().sum(function() { return  this.Price(); }) )</param>
        var len = this.length;
        if (typeof fnValueProvider != "function")
            throw new TypeError();

        if (len == 0)
            return 0;

        var sum = 0;
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                sum += fnValueProvider.call(this[i]);
            }
        }

        return sum / len;
    };
}

if (!Array.prototype.groupBy)
{
    Array.prototype.groupBy = function (fnValueProvider, args)
    {/// <summary>groups element in the array based on a param </summary>
        /// <param name="fnPredicate" type="function">search function delegete ( roles.groupBy(function() { return  this.RoleID(); })))</param>
        var len = this.length;
        if (typeof fnValueProvider != "function")
            throw new TypeError();

        if (len == 0)
            return 0;

        var groupedList = [];

        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var addNewGroup = true;
                var groupLen = groupedList.length;

                var key = fnValueProvider.call(this[i]);

                for (var j = 0; j < groupLen; j++)
                {
                    if (groupedList[j].Key == key)
                    {
                        addNewGroup = false;
                        groupedList[j].Items.push(this[i]);
                    }
                }

                if (addNewGroup)
                {
                    var newGroup = new Object();
                    newGroup.Key = key
                    newGroup.Items = [];
                    newGroup.Items.push(this[i]);
                    groupedList.push(newGroup);
                }
            }
        }

        return groupedList;
    };
}

//items().count(function () {return this.Price == price; }); 
if (!Array.prototype.count)
{
    Array.prototype.count = function (fnPredicate, args)
    {/// <summary>count the number of element that match the search  </summary>
        /// <param name="fnPredicate" type="function">search function delegete (items().count(function () {return this.Price == price; }); )</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();
        var count = 0;
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var match = fnPredicate.call(this[i]);
                if (match)
                    count++;
            }
        }

        return count;
    };
}
//items().any(function () {return this.Price == price; }); 
if (!Array.prototype.any)
{
    Array.prototype.any = function (fnPredicate, args)
    {/// <summary>determines if atlest one element in the array marchs the search   </summary>
        /// <param name="fnPredicate" type="function">search function delegete (items().any(function () {return this.Price == price; });)</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var match = fnPredicate.call(this[i]);
                if (match)
                    return true;
            }
        }

        return false;
    };
}

if (!Array.prototype.all)
{
    Array.prototype.all = function (fnPredicate, args)
    {/// <summary>determines if all element in the array marchs the search    </summary>
        /// <param name="fnPredicate" type="function">search function delegete (items().all(function () {return this.Price == price; });)</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var match = fnPredicate.call(this[i]);
                if (!match)
                    return false;
            }
        }

        return true;
    };
}

if (!Array.prototype.Remove)
{
    Array.prototype.Remove = function (fnPredicate, args)
    {
        /// <summary>removes all element from the array that matchs the search</summary>
        /// <param name="fnPredicate" type="function">search function delegete</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        var matches = [];
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var match = fnPredicate.call(this[i]);
                if (!match)
                {
                    //return this[i];
                    matches.push(this[i]);
                }
            }
        }
        return matches;
    }
}

if (!Array.prototype.swapItems)
{
    Array.prototype.swapItems = function (a, b)
    {
        /// <summary>swaps to items in the array</summary>
        /// <param name="a" type="number">index of first item</param>
        /// <param name="b" type="number">index of second param</param>
        var temp = this[a];
        this[a] = this[b];
        this[b] = temp;
    }
}

if (!Array.prototype.distinct)
{
    Array.prototype.distinct = function ()
    {/// <summary>removes duplicates entitys from the array (checks all the propertys)    </summary>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        var matches = [];
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                if (!(this[i] in matches))
                {
                    matches.push(this[i]);
                }
            }
        }
        return matches;

    }
}

if (!Array.prototype.distinctByKey)
{
    Array.prototype.distinctByKey = function (fnPredicate, args)
    {/// <summary>removes duplicates entitys from the array (check a specific key)    </summary>
        /// <param name="fnPredicate" type="function">search function delegete (items().distinct(function () {return this.ID });)</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        var matches = [];
        var keys = [];
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                var key = fnPredicate.call(this[i]);

                var innerLen = keys.length;
                var wasFound = false;
                for (var j = 0; j < innerLen; j++)
                {
                    if (keys[j] == key)
                    {
                        wasFound = true;
                    }
                }

                if (!wasFound)
                {
                    matches.push(this[i]);
                    keys.push(key);// saves the key so will know if we added it
                }
            }
        }
        return matches;

    }
}

if (!Array.prototype.union)
{
    Array.prototype.union = function (array)
    {/// <summary>unite to array to a single array , the returned array is distinct   </summary>
        /// <param name="array" type="array">a second array to add to our array</param>
        var len = array.length;


        var matches = this;
        for (var i = 0; i < len; i++)
        {
            matches.push(array[i]);
        }

        matches = matches.distinct();

        return matches;
    }
}

if (!Array.prototype.unionAll)
{
    Array.prototype.unionAll = function (array)
    {/// <summary>unite to array to a single array , the returned array is not distinct    </summary>
        /// <param name="array" type="array">a second array to add to our array</param>
        var len = array.length;


        var matches = this;
        for (var i = 0; i < len; i++)
        {
            matches.push(array[i]);
        }

        return matches;
    }
}

if (!Array.prototype.Intersect)
{
    Array.prototype.intersect = function (array)
    {/// <summary>return an array the has the common element of 2  given array   </summary>
        /// <param name="array" type="array">a second array </param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        var matches = [];
        for (var i = 0; i < len; i++)
        {
            if (i in this)
            {
                if (this[i] in array)
                {
                    matches.push(this[i]);
                }
            }
        }

        return matches;

    }
}

if (!Array.prototype.orderBy)
{
    Array.prototype.orderBy = function (fnPredicate)
    {/// <summary>orders the array by a specific proprety  </summary>
        /// <param name="fnPredicate" type="function">order function delegete (items().orderBy(function () {return this.ID });)</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        var array = this;
        var temp = {};
        for (var i = 0; i < len; i++)
        {
            for (var j = 0; j < len - 1; j++)
            {
                if (fnPredicate.call(this[j]) > fnPredicate.call(this[j + 1]))
                {
                    temp = array[j];

                    array[j] = array[j + 1];

                    array[j + 1] = temp;
                }
            }
        }

        return array;

    }
}

if (!Array.prototype.orderByDescending)
{
    Array.prototype.orderByDescending = function (fnPredicate)
    {/// <summary>orders the array by a specific proprety  </summary>
        /// <param name="fnPredicate" type="function">order function delegete (items().orderBy(function () {return this.ID });)</param>
        var len = this.length;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        var array = this;
        var temp = {};
        for (var i = 0; i < len; i++)
        {
            for (var j = 0; j < len - 1; j++)
            {
                if (fnPredicate.call(this[j]) < fnPredicate.call(this[j + 1]))
                {
                    temp = array[j];

                    array[j] = array[j + 1];

                    array[j + 1] = temp;
                }
            }
        }

        return array;

    }
}

if (!Array.prototype.skip)
{
    Array.prototype.skip = function (counter)
    {
        /// <summary>skips the first [counter] of objects from the array </summary>
        /// <param name="counter" type="number">number of objects to skip</param>
        var len = this.length;

        var matches = [];
        for (var i = counter; i < len; i++)
        {
            if (i in this)
            {
                matches.push(this[i]);
            }
        }
        return matches;
    }
}
if (!Array.prototype.skipWhile)
{
    Array.prototype.skipWhile = function (fnPredicate)
    {
        /// <summary>skips the first objects in the array that matches the predicate </summary>
        /// <param name="fnPredicate" type="function">search function delegete</param>
        var len = this.length;
        var counter = 0;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        for (var i = 0; i < len; i++)
        {
            var match = fnPredicate.call(this[i]);
            if (!match)
            {
                counter = i;
                break;
            }
        }

        var matches = [];
        for (var i = counter; i < len; i++)
        {
            if (i in this)
            {
                matches.push(this[i]);
            }
        }
        return matches;
    }
}

if (!Array.prototype.take)
{
    Array.prototype.take = function (counter)
    {
        /// <summary>takes the first [counter] of objects from the array</summary>
        /// <param name="counter" type="number">number of objects to take</param>

        var matches = [];
        for (var i = 0; i < counter; i++)
        {
            if (i in this)
            {
                matches.push(this[i]);
            }
        }
        return matches;
    }
}

if (!Array.prototype.takeWhile)
{
    Array.prototype.takeWhile = function (fnPredicate)
    {
        /// <summary>takes the first objects in the array that matches the predicate </summary>
        /// <param name="fnPredicate" type="function">search function delegete</param>
        var len = this.length;
        var counter = 0;
        if (typeof fnPredicate != "function")
            throw new TypeError();

        for (var i = 0; i < len; i++)
        {
            var match = fnPredicate.call(this[i]);
            if (!match)
            {
                counter = i;
                break;
            }
        }

        var matches = [];
        for (var i = 0; i < counter; i++)
        {
            if (i in this)
            {
                matches.push(this[i]);
            }
        }
        return matches;
    }
}

if (!Array.prototype.forEach)
{
    Array.prototype.forEach = function (callback, thisArg)
    {
        /// <summary>add the forEach loop on array </summary>
        /// <param name="fnPredicate" type="function">arr.forEach(function (element, index, array){}) ]</param>
        var T, counter;

        if (this == null)
        {
            throw new TypeError(" this is null or not defined");
        }

        // 1. Let O be the result of calling ToObject passing the |this| value as the argument.
        var Obj = Object(this);

        // 2. Let lenValue be the result of calling the Get internal method of O with the argument "length".
        // 3. Let len be ToUint32(lenValue).
        var len = Obj.length >>> 0;

        // 4. If IsCallable(callback) is false, throw a TypeError exception.
        // See: http://es5.github.com/#x9.11
        if (typeof callback !== "function")
        {
            throw new TypeError(callback + " is not a function");
        }

        // 5. If thisArg was supplied, let T be thisArg; else let T be undefined.
        if (arguments.length > 1)
        {
            T = thisArg;
        }

        // 6. Let k be 0
        counter = 0;
        // 7. Repeat, while counter < len
        while (counter < len)
        {
            var kValue;
            if (counter in Obj)
            {
                // i. Let kValue be the result of calling the Get internal method of O with argument Pk.
                kValue = Obj[counter];
                // ii. Call the Call internal method of callback with T as the this value and
                // argument list containing kValue, counter, and Obj.
                callback.call(T, kValue, counter, Obj);
            }
            // d. Increase k by 1.
            counter++;
        }
        // 8. return undefined
    };
}

if (!Array.prototype.reduce)
{
    // [0, 1, 2, 3].reduce(function (val, b,i,array) {return val.toString() + "," + b.toString();});
    Array.prototype.reduce = function (callback, initialValue)
    {
        'use strict';
        if (null === this || 'undefined' === typeof this)
        {
            throw new TypeError('Array.prototype.reduce called on null or undefined');
        }

        if ('function' !== typeof callback)
        {
            throw new TypeError(callback + ' is not a function');
        }

        var t = Object(this);
        var len = this.length;
        var value;
        var k = 0;

        if (!initialValue)
        {
            if (arguments.length >= 2)
            {
                value = arguments[1];
            }
            else
            {
                while (k < len && !k in t)
                {
                    k++;
                }

                if (k >= len)
                {
                    throw new TypeError('Reduce of empty array with no initial value');
                }

                value = t[k++];
            }
        }
        else
        {
            value = initialValue;
        }

        for (; k < len ; k++)
        {
            if (k in t)
            {
                value = callback(value, t[k], k, t);
            }
        }

        return value;
    };
}

id(!Array.prototype.toDictionary )
{ 	
	/// <summary>Creates an object from an array according to a specified key selector function </summary>
	/// <param name="fnPredicate" type="function">search function delegete</param> (var dic = arr.toDictionary(function(t){ return "Num" + t }, function(u){ return u }); )
	Array.prototype.toDictionary = function (keySelector, valueSelector) {
		var o = {};
		var l = this.length;
		while (l-- > 0) {
			var key = keySelector(this[l]);
			if (key == null || key == "") continue;
			o[key] = valueSelector(this[l]);
		}
		return o;
	};  
}