/**
	@ dateutil 日期时间公共函数处理
	@author：longfc
	@version 2019-05-28
 */
layui.define(["util"], function (exports) {
    "use strict";

    var $ = layui.jquery;
    var util = layui.util;

    var dateutil = {
        //获得某年某月的总天数 
        getMonthDays: function (year, month) {
            var monthStartDate = new Date(year, month, 1);
            var monthEndDate = new Date(year, month + 1, 1);
            var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);//格式转换
            return days;
        },
        /**
         * 计算2日期之间的天数，用的是比较毫秒数的方法
         * 传进来的日期要么是Date类型，要么是yyyy-MM-dd格式的字符串日期
         * @param date1 日期一
         * @param date2 日期二
         */
        countDays: function (date1, date2) {
            var fmt = 'yyyy-MM-dd';
            // 将日期转换成字符串，转换的目的是去除“时、分、秒”
            if (date1 instanceof Date && date2 instanceof Date) {
                date1 = this.format(fmt, date1);
                date2 = this.format(fmt, date2);
            }
            if (typeof date1 === 'string' && typeof date2 === 'string') {
                date1 = this.parse(date1, fmt);
                date2 = this.parse(date2, fmt);
                return (date1.getTime() - date2.getTime()) / (1000 * 60 * 60 * 24);
            }
            else {
                console.error('参数格式无效！');
                return 0;
            }
        },
        //获得某年某月的开始日期
        getMonthStartDate: function (year, month) {
            var monthStartDate = new Date(year, month, 1);
            return util.toDateString(monthStartDate, 'yyyy-MM-dd');//返回某月第一天
        },
        //获得某年某月的结束日期 
        getMonthEndDate: function (year, month) {
            var days = this.getMonthDays(year, month);//获取某月总共有多少天
            var monthEndDate = new Date(year, month, days);
            return util.toDateString(monthEndDate, 'yyyy-MM-dd');//返回某月结束时间
        },
        //获得某周的开始日期　　
        getWeekStartDate: function (year, month, paraDay, paraDayOfWeek) {
            var weekStartDate = new Date(year, month, paraDay + 1 - paraDayOfWeek);
            return util.toDateString(weekStartDate, 'yyyy-MM-dd');
        },
        //获得某周的结束日期　　
        getWeekEndDate: function (year, month, paraDay, paraDayOfWeek) {
            var weekEndDate = new Date(year, month, paraDay + (7 - paraDayOfWeek));
            return util.toDateString(weekEndDate, 'yyyy-MM-dd');
        },
        //获得上月开始时间　
        getLastMonthStartDate: function () {
            var date = new Date();
            var year = date.getFullYear();//本年
            var month = date.getMonth();//上月
            var lastMonthStartDate = new Date(year, month, 1);
            return util.toDateString(lastMonthStartDate, 'yyyy-MM-dd');
        },
        //获得上月结束时间　
        getLastMonthEndDate: function () {
            var date = new Date();
            var year = date.getFullYear();//本年
            var month = date.getMonth();//上月
            var days = this.getMonthDays(year, month);//获取某月总共有多少天
            var lastMonthEndDate = new Date(year, month, days);
            return util.toDateString(lastMonthEndDate, 'yyyy-MM-dd');
        },
        //获得某季度的开始日期　　
        getQuarterStartDate: function (paraYear, paraSeason) {
            switch (paraSeason) {
                case '1': return paraYear + "-01-01";
                case '2': return paraYear + "-04-01";
                case '3': return paraYear + "-07-01";
                case '4': return paraYear + "-10-01";
            }
        },
        //获得某季度的结束日期　　
        getQuarterEndDate: function (paraYear, paraSeason) {
            switch (paraSeason) {
                case '1': return paraYear + "-03-31";
                case '2': return paraYear + "-06-30";
                case '3': return paraYear + "-09-30";
                case '4': return paraYear + "-12-31";
            }
        },
        /**获取时间对象,不能转为时间的返回null*/
        getDate: function (value) {
            if (!value) return null;

            var type = $.type(value),
                date = null;

            if (type == 'date') {
                date = Ext.clone(value);
            } else if (type == 'string') {
                if (value.length == 26 && value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
                    // /Date(1413993600000+0800)/
                    value = parseInt(value.slice(6, -7));
                } else if (value.length == 27 && value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
                    // /Date(1413993600000+0800)/
                    value = parseInt(value.slice(6, -7));
                } else {
                    value = value.replace(/-/g, '/');
                }
                date = new Date(value);
            } else if (type == 'number') {
                date = new Date(value);
            }

            var isDate = (Date.parse(date) == Date.parse(date));
            if (isDate) return date;
            return null;
        },
        /**获取距离value这个时间num天的时间对象;
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
        }
    };

    //暴露接口
    exports('dateutil', dateutil);
});