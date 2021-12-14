/**
	@ statistical.modules.dateutil 日期时间处理
	@author：longfc
	@version 2019-05-28
 */
layui.extend({

}).define(["util"], function (exports) {
    "use strict";

    var util = layui.util;
    
    var dateutil = {
        //格式化日期
        formatDate: function (date, format) {
            var myyear = date.getFullYear();
            var mymonth = date.getMonth() + 1;
            var myweekday = date.getDate();
            if (mymonth < 10) {
                mymonth = "0" + mymonth;
            }
            if (myweekday < 10) {
                myweekday = "0" + myweekday;
            }
            return (myyear + "-" + mymonth + "-" + myweekday);
        },
        //获得某年某月的总天数 
        getMonthDays: function (year, month) {
            var monthStartDate = new Date(year, month, 1);
            var monthEndDate = new Date(year, month + 1, 1);
            var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);//格式转换
            return days;
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
            return util.toDateString(weekStartDate,'yyyy-MM-dd');
        },
        //获得某周的结束日期　　
        getWeekEndDate: function (year, month, paraDay, paraDayOfWeek) {
            var weekEndDate = new Date(year, month, paraDay + (7 - paraDayOfWeek));
            return util.toDateString(weekEndDate,'yyyy-MM-dd');
        },
        //获得上月开始时间　
        getLastMonthStartDate: function () {
            var date = new Date();
            var year = date.getFullYear();//本年
            var month =date.getMonth();//上月
            var lastMonthStartDate = new Date(year, month, 1);
            return util.toDateString(lastMonthStartDate,'yyyy-MM-dd');
        },
        //获得上月结束时间　
        getLastMonthEndDate: function () {
            var date = new Date();
            var year = date.getFullYear();//本年
            var month =date.getMonth();//上月
            var days = this.getMonthDays(year, month);//获取某月总共有多少天
            var lastMonthEndDate = new Date(year, month, days);
            return util.toDateString(lastMonthEndDate,'yyyy-MM-dd');
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
        }
    };

    //暴露接口
    exports('dateutil', dateutil);
});