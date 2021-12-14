/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**时间处理*/
JcallShell.Date = {
    /**获取时间对象,不能转为时间的返回null*/
    getDate: function (value) {
        if (!value) return null;

        var type = typeof value,
			date = null;

        if (type == 'number') {
            date = new Date(value);
        } else if (type == 'string') {
            if ((value.length == 26 || value.length == 27) &&
				value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
                // /Date(1413993600000+0800)/ /Date(-1413993600000+0800)/
                value = parseInt(value.slice(6, -7));
            } else {
                value = value.replace(/-/g, '/');
            }
            date = new Date(value);
        } else if (Object.prototype.toString.call(value) == '[object Date]') {
            date = new Date(value.getTime());//复制
        }

        var isDate = (Date.parse(date) == Date.parse(date));

        return isDate ? date : null;
    },
    /**校验对象是否是时间*/
    isValid: function (value) {
        var date = this.getDate(value);
        return date ? true : false;
    },
    /**
	 * 获取距离value这个时间num天的时间对象;
	 * @param {date/string/number} value 当前时间
	 * @param {number} num 默认为1,可以负数,例如-1就是昨天,1是明天;
	 * @return {}
	 */
    getNextDate: function (value, num) {
        var date = this.getDate(value);
        if (!value) return null;

        var n = isNaN(num) ? 1 : parseInt(num);

        date.setDate(date.getDate() + n);

        return date;
    },
    /**
	 * 获取时间字符串
	 * @param {date/string/number} value 当前时间
	 * @param {boolean} onlyDate 是否只显示日期
	 * @param {boolean} hasMilliseconds 带毫秒
	 * @param {boolean} hasDay 带星期
	 */
    toString: function (value, onlyDate, hasMilliseconds, hasDay) {
        var v = this.getDate(value);
        if (!v) return null;

        var info = '',
			year = v.getFullYear() + '',
			month = (v.getMonth() + 1) + '',
			date = v.getDate() + '';

        month = month.length == 1 ? '0' + month : month;
        date = date.length == 1 ? '0' + date : date;

        info = year + '-' + month + '-' + date;

        if (!onlyDate) {
            var hours = v.getHours() + '',
				minutes = v.getMinutes() + '',
				seconds = v.getSeconds() + '';

            hours = hours.length == 1 ? '0' + hours : hours;
            minutes = minutes.length == 1 ? '0' + minutes : minutes;
            seconds = seconds.length == 1 ? '0' + seconds : seconds;

            info += ' ' + hours + ':' + minutes + ':' + seconds;
        }
        if (hasMilliseconds) {
            info += ' ' + v.getMilliseconds();
        }

        if (hasDay) {
            var day = v.getDay();
            var text = "星期";
            switch (day) {
                case 0:
                    text += "日";
                    break;
                case 1:
                    text += "一";
                    break;
                case 2:
                    text += "二";
                    break;
                case 3:
                    text += "三";
                    break;
                case 4:
                    text += "四";
                    break;
                case 5:
                    text += "五";
                    break;
                case 6:
                    text += "六";
                    break;

            }
            info += " " + text;
        }

        return info;
    },
    /**将时间转化为后台需要的格式,例如:\/Date(1359779125000)\/*/
    toServerDate: function (value) {
        var v = this.getDate(value);
        if (!v) return null;

        return "\/Date(" + v.getTime() + "+0000)\/";
    },
    /**校验是否一个月的第一天*/
    isMonthFirstDate: function (value) {
        var v = this.getDate(value);
        if (!v) return false;

        //每个月的1号就是第一天
        if (v.getDate() == 1) return true;

        return false;
    },
    /**校验是否一个月的最后一天*/
    isMonthLastDay: function (value) {
        var v = this.getDate(value);
        if (!v) return false;

        var month = v.getMonth();
        var month2 = this.getNextDate(v).getMonth();

        //符合条件：当天时间加上一天就是下个月的第一天
        if ((month2 - month - 1) % 12 == 0) return true;

        return false;
    },
    /**检验是否整月*/
    isFullMonth: function (start, end) {
        var s = this.getDate(start);
        var e = this.getNextDate(end);

        //start < end
        if (Date.parse(s) >= Date.parse(e)) return false;

        //start的日期 = end的日期
        if (s.getDate() != e.getDate()) return false;

        return true;
    },
    /**获取一个月的第一天*/
    getMonthFirstDate: function (year, month, toString) {
        var m = ((month + '').length == 1 ? '0' : '') + month;
        var date = year + '-' + m + '-01';

        if (!toString) date = this.getDate(date);

        return date;
    },
    /**获取一个月的最后一天*/
    getMonthLastDate: function (year, month, toString) {
        var date = new Date(year, month, 0);

        if (toString) date = this.toString(date, true);

        return date;
    }
};