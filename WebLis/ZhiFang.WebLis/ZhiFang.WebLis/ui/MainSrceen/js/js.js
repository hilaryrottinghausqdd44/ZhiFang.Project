$(window).load(function () {
    $(".loading").fadeOut()
})
$(function () {
    var href = window.document.location.href;
    var pathname = window.document.location.pathname;
    var pos = href.indexOf(pathname);
    var localhostPaht = href.substring(0, pos);
    var projectName = pathname.substring(0, pathname.substr(1).indexOf('/') + 1);
    var url = localhostPaht + projectName + "/ServiceWCF/StatisticsService.svc/StatisticsMainScreen";
    var SearchTime = "1800000"; //查询时间间隔  毫秒为单位 默认半小时 通过url传值SearchTime分钟数也可以
    function init() {
        getParams();
        SearchData();
        setInterval(function () {
            SearchData();
        }, SearchTime);
    };
    //获得参数
    function getParams() {
        var me = this,
            params = location.search ? location.search.split("?")[1].split("&") : "";
        if (!params) return;
        //参数赋值
            $.each(params, function (j, itemJ) {
                if ("SearchTime".toUpperCase() == itemJ.split("=")[0].toUpperCase()) {
                    var searchmin = decodeURIComponent(itemJ.split("=")[1]);
                    SearchTime = searchmin * 60 * 1000;
                    return false;
                }
            });
    };
    /**判断数据类型*/
    function typeOf(value) {
        var type,
            typeToString;

        if (value === null) return 'null';

        type = typeof value;

        if (type === 'undefined' || type === 'string' || type === 'number' || type === 'boolean') {
            return type;
        }

        typeToString = Object.prototype.toString.call(value);

        switch (typeToString) {
            case '[object Array]': return 'array';
            case '[object Date]': return 'date';
            case '[object Boolean]': return 'boolean';
            case '[object Number]': return 'number';
            case '[object RegExp]': return 'regexp';
        }

        if (type === 'function') return 'function';

        if (type === 'object') {
            if (value.nodeType !== undefined) {
                if (value.nodeType === 3) {
                    return (/\S/).test(value.nodeValue) ? 'textnode' : 'whitespace';
                } else {
                    return 'element';
                }
            }
            return 'object';
        }
        //<debug error>
        Shell.util.Msg.showLog('无法确定指定的值的类型,这最有可能是一个BUG!该值是:' + value, true);
        //</debug>
    };
    /**克隆数据*/
    function clone(item) {
        var type, i, j, k, clone, key;

        if (item === null || item === undefined) {
            return item;
        }

        if (item.nodeType && item.cloneNode) {
            return item.cloneNode(true);
        }

        type = Object.prototype.toString.call(item);

        if (type === '[object Date]') {//时间
            return new Date(item.getTime());
        }

        if (type === '[object Array]') {//数组
            i = item.length;
            clone = [];
            while (i--) {
                clone[i] = Shell.util.clone(item[i]);
            }
        }
        //对象
        else if (type === '[object Object]' && item.constructor === Object) {
            clone = {};

            for (key in item) {
                clone[key] = Shell.util.clone(item[key]);
            }

            if (enumerables) {
                for (j = enumerables.length; j--;) {
                    k = enumerables[j];
                    clone[k] = item[k];
                }
            }
        }

        return clone || item;
    };
    function getDate(value) {
        if (!value) return null;

        var type = typeOf(value),
            date = null;

        if (type == 'date') {
            date = clone(value);
        } else if (type == 'string') {
            if (value.length == 26 && value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
                // /Date(1413993600000+0800)/
                value = parseInt(value.slice(6, -7));
            } else if (value.length == 27 && value.slice(0, 6) == "/Date(" && value.slice(-2) == ")/") {
                // /Date(-1413993600000+0800)/
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
    };
    function getNextDate(value, num) {
        var date = getDate(value);
        if (!date) return null;

        var n = isNaN(num) ? 1 : parseInt(num);

        date.setDate(date.getDate() + n);

        return date;
    };
    function toString(value, onlyDate) {
        var date = getDate(value);
        if (!date) return null;

        var info = '',
            year = date.getFullYear() + '',
            month = (date.getMonth() + 1) + '',
            day = date.getDate() + '';

        month = month.length == 1 ? '0' + month : month;
        day = day.length == 1 ? '0' + day : day;

        info = year + '-' + month + '-' + day;
        return info;
    };
    function SearchData() {
        echarts_1();//全年报告量统计饼图
        echarts_2(); //检验项目量排名
        echarts_3();//全年报告量统计曲线图
        echarts_4();//全年检验量统计
        echarts_5();//检验完成量统计
        zb();//检验完成
        
        SampleWLMsg();//标本物流信息
        samplesortall();//检验量排名(总)
        sjSampleCountSort();//送检量排名
    };
   //全年报告量统计饼图
    function echarts_1() {
        var url1 = url + "?DataType=7&Limit=5";
        url1 += "&StartDateTime=" + toString(getNextDate(new Date(), -30));
        url1 += "&EndDateTime=" + toString(new Date());
        $.ajax({
            url: url1,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    var ResultDataValue = JSON.parse(data.ResultDataValue);
                    console.log("全年报告量统计饼图", ResultDataValue);
                    var LabName = [];
                    var ReportCount = [];
                    for (var i = 0; i < ResultDataValue.length; i++) {
                        LabName.push(ResultDataValue[i].LabName);
                        ReportCount.push({ "value": ResultDataValue[i].ReportCount, name: ResultDataValue[i].LabName});
                    }
                    // 基于准备好的dom，初始化echarts实例
                    var myChart = echarts.init(document.getElementById('echart1'));
                    option = {
                        tooltip: {
                            trigger: 'item',
                            formatter: "{b} : {c} ({d}%)"
                        },
                        legend: {
                            right: 0,
                            top: 30,
                            height: 160,
                            itemWidth: 10,
                            itemHeight: 10,
                            itemGap: 10,
                            textStyle: {
                                color: 'rgba(255,255,255,.6)',
                                fontSize: 12
                            },
                            orient: 'vertical',
                            data: LabName
                        },
                        calculable: true,
                        series: [
                            {
                                name: ' ',
                                color: ['#62c98d', '#2f89cf', '#4cb9cf', '#53b666', '#62c98d', '#205acf', '#c9c862', '#c98b62', '#c962b9', '#7562c9', '#c96262', '#c25775', '#00b7be'],
                                type: 'pie',
                                radius: [30, 70],
                                center: ['35%', '50%'],
                                roseType: 'radius',
                                label: {
                                    normal: {
                                        show: true
                                    },
                                    emphasis: {
                                        show: true
                                    }
                                },

                                lableLine: {
                                    normal: {
                                        show: true
                                    },
                                    emphasis: {
                                        show: true
                                    }
                                },

                                data: ReportCount
                            },
                        ]
                    };

                    // 使用刚指定的配置项和数据显示图表。
                    myChart.setOption(option);
                    window.addEventListener("resize", function () {
                        myChart.resize();
                    });
                }
            },
            error: function (data) {
            }
        });
        
    }
    //检验项目量排名
    function echarts_2() {
        var url1 = url + "?DataType=9&Limit=5";
        url1 += "&StartDateTime=" + toString(getNextDate(new Date(), -30));
        url1 += "&EndDateTime=" + toString(new Date());
        $.ajax({
            url: url1,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    var ResultDataValue = JSON.parse(data.ResultDataValue);
                    console.log("检验项目量排名", ResultDataValue);
                    var ItemName = [];
                    var TestItemCount = [];
                    for (var i = 0; i < ResultDataValue.length; i++) {
                        ItemName.push(ResultDataValue[i].ItemName);
                        TestItemCount.push({ "value": ResultDataValue[i].TestItemCount, name: ResultDataValue[i].ItemName });
                    }
                    TestItemCount.push(
                        { value: 0, name: "", label: { show: false }, labelLine: { show: false } },
                        { value: 0, name: "", label: { show: false }, labelLine: { show: false } },
                        { value: 0, name: "", label: { show: false }, labelLine: { show: false } },
                        { value: 0, name: "", label: { show: false }, labelLine: { show: false } },
                        { value: 0, name: "", label: { show: false }, labelLine: { show: false } });
                    // 基于准备好的dom，初始化echarts实例
                    var myChart = echarts.init(document.getElementById('echart2'));

                    option = {
                        tooltip: {
                            trigger: 'item',
                            formatter: "{b} : {c} ({d}%)"
                        },
                        legend: {

                            top: '15%',
                            data: ItemName,
                            icon: 'circle',
                            textStyle: {
                                color: 'rgba(255,255,255,.6)',
                            }
                        },
                        calculable: true,
                        series: [{
                            name: '',
                            color: ['#62c98d', '#2f89cf', '#4cb9cf', '#53b666', '#62c98d', '#205acf', '#c9c862', '#c98b62', '#c962b9', '#c96262'],
                            type: 'pie',
                            //起始角度，支持范围[0, 360]
                            startAngle: 0,
                            //饼图的半径，数组的第一项是内半径，第二项是外半径
                            radius: [51, 100],
                            //支持设置成百分比，设置成百分比时第一项是相对于容器宽度，第二项是相对于容器高度
                            center: ['50%', '45%'],

                            //是否展示成南丁格尔图，通过半径区分数据大小。可选择两种模式：
                            // 'radius' 面积展现数据的百分比，半径展现数据的大小。
                            //  'area' 所有扇区面积相同，仅通过半径展现数据大小
                            roseType: 'area',
                            //是否启用防止标签重叠策略，默认开启，圆环图这个例子中需要强制所有标签放在中心位置，可以将该值设为 false。
                            avoidLabelOverlap: false,
                            label: {
                                normal: {
                                    show: true,
                                    //  formatter: '{c}辆'
                                },
                                emphasis: {
                                    show: true
                                }
                            },
                            labelLine: {
                                normal: {
                                    show: true,
                                    length2: 1,
                                },
                                emphasis: {
                                    show: true
                                }
                            },
                            data: TestItemCount
                        }]
                    };

                    // 使用刚指定的配置项和数据显示图表。
                    myChart.setOption(option);
                    window.addEventListener("resize", function () {
                        myChart.resize();
                    });
                }
            }
        });
        
    }
    //全年报告量统计曲线图
    function echarts_3() {
        var url1 = url + "?DataType=8";
        $.ajax({
            url: url1,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    var ResultDataValue = JSON.parse(data.ResultDataValue);

                    console.log("全年报告量统计曲线图", ResultDataValue);var reportcount = [];
                    var sendbarcodecount = [];
                    for (var i = 0; i < ResultDataValue.length; i++) {
                        sendbarcodecount.push(ResultDataValue[i].SendBarCodeCount);
                        reportcount.push(ResultDataValue[i].ReportCount);
                    }
                    // 基于准备好的dom，初始化echarts实例
                    var myChart = echarts.init(document.getElementById('echart3'));

                    option = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {
                                lineStyle: {
                                    color: '#57617B'
                                }
                            }
                        },
                        legend: {

                            //icon: 'vertical',
                            data: ['送检量', '报告量'],
                            //align: 'center',
                            // right: '35%',
                            top: '0',
                            textStyle: {
                                color: "#fff"
                            },
                            // itemWidth: 15,
                            // itemHeight: 15,
                            itemGap: 20,
                        },
                        grid: {
                            left: '0',
                            right: '20',
                            top: '10',
                            bottom: '20',
                            containLabel: true
                        },
                        xAxis: [{
                            type: 'category',
                            boundaryGap: false,
                            axisLabel: {
                                show: true,
                                textStyle: {
                                    color: 'rgba(255,255,255,.6)'
                                }
                            },
                            axisLine: {
                                lineStyle: {
                                    color: 'rgba(255,255,255,.1)'
                                }
                            },
                            data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                        }, {
                        }],
                        yAxis: [{
                            axisLabel: {
                                show: true,
                                textStyle: {
                                    color: 'rgba(255,255,255,.6)'
                                }
                            },
                            axisLine: {
                                lineStyle: {
                                    color: 'rgba(255,255,255,.1)'
                                }
                            },
                            splitLine: {
                                lineStyle: {
                                    color: 'rgba(255,255,255,.1)'
                                }
                            }
                        }],
                        series: [{
                            name: '送检量',
                            type: 'line',
                            smooth: true,
                            symbol: 'circle',
                            symbolSize: 5,
                            showSymbol: false,
                            lineStyle: {
                                normal: {
                                    width: 2
                                }
                            },
                            areaStyle: {
                                normal: {
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                        offset: 0,
                                        color: 'rgba(24, 163, 64, 0.3)'
                                    }, {
                                        offset: 0.8,
                                        color: 'rgba(24, 163, 64, 0)'
                                    }], false),
                                    shadowColor: 'rgba(0, 0, 0, 0.1)',
                                    shadowBlur: 10
                                }
                            },
                            itemStyle: {
                                normal: {
                                    color: '#cdba00',
                                    borderColor: 'rgba(137,189,2,0.27)',
                                    borderWidth: 12
                                }
                            },
                            data: sendbarcodecount
                        }, {
                            name: '报告量',
                            type: 'line',
                            smooth: true,
                            symbol: 'circle',
                            symbolSize: 5,
                            showSymbol: false,
                            lineStyle: {
                                normal: {
                                    width: 2
                                }
                            },
                            areaStyle: {
                                normal: {
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                        offset: 0,
                                        color: 'rgba(39, 122,206, 0.3)'
                                    }, {
                                        offset: 0.8,
                                        color: 'rgba(39, 122,206, 0)'
                                    }], false),
                                    shadowColor: 'rgba(0, 0, 0, 0.1)',
                                    shadowBlur: 10
                                }
                            },
                            itemStyle: {
                                normal: {
                                    color: '#277ace',
                                    borderColor: 'rgba(0,136,212,0.2)',
                                    borderWidth: 12
                                }
                            },
                                data: reportcount
                        }]
                    };

                    // 使用刚指定的配置项和数据显示图表。
                    myChart.setOption(option);
                    window.addEventListener("resize", function () {
                        myChart.resize();
                    });
                }
            }
        });
       
    }
    //全年检验量统计
    function echarts_4() {
        var url1 = url + "?DataType=5";
        $.ajax({
            url: url1,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    var ResultDataValue = JSON.parse(data.ResultDataValue);

                    console.log("全年检验量统计", ResultDataValue);
                    var ReceiveBarCodeCount = [];
                    var TestBarCodeCount = [];
                    var TestFinishRate = [];
                    for (var i = 0; i < ResultDataValue.length; i++) {
                        ReceiveBarCodeCount.push(ResultDataValue[i].ReceiveBarCodeCount);
                        TestBarCodeCount.push(ResultDataValue[i].TestBarCodeCount);
                        TestFinishRate.push(ResultDataValue[i].TestFinishRate);
                    }
                    var Receivemax = 0;
                    for (var i = 0; i < ReceiveBarCodeCount.length; i++) {
                        if (Receivemax < ReceiveBarCodeCount[i]) {
                            Receivemax = ReceiveBarCodeCount[i];
                        }
                    }
                    // 基于准备好的dom，初始化echarts实例
                    var myChart = echarts.init(document.getElementById('echart4'));
                    option = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {
                                lineStyle: {
                                    color: '#57617B'
                                }
                            }
                        },
                        "legend": {

                            "data": [
                                { "name": "接检量" },
                                { "name": "完成量" },
                                { "name": "完成率" }
                            ],
                            "top": "0%",
                            "textStyle": {
                                "color": "rgba(255,255,255,0.9)"//图例文字
                            }
                        },

                        "xAxis": [
                            {
                                "type": "category",

                                data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
                                axisLine: { lineStyle: { color: "rgba(255,255,255,.1)" } },
                                axisLabel: {
                                    textStyle: { color: "rgba(255,255,255,.6)", fontSize: '14', },
                                },

                            },
                        ],
                        "yAxis": [
                            {
                                "type": "value",
                                "name": "样本量",
                                "min": 0,
                                "max": Receivemax,
                                "interval": Math.pow(10, Receivemax.toString().length - 1) * (Receivemax.toString().length - 1),
                                "axisLabel": {
                                    "show": true,

                                },
                                axisLine: { lineStyle: { color: 'rgba(255,255,255,.4)' } },//左线色

                            },
                            {
                                "type": "value",
                                "name": "完成率",
                                "show": true,
                                "axisLabel": {
                                    "show": true,

                                },
                                axisLine: { lineStyle: { color: 'rgba(255,255,255,.4)' } },//右线色
                                splitLine: { show: true, lineStyle: { color: "#001e94" } },//x轴线
                            },
                        ],
                        "grid": {
                            "top": "10%",
                            "right": "30",
                            "bottom": "30",
                            "left": "30",
                        },
                        "series": [
                            {
                                "name": "接检量",

                                "type": "bar",
                                "data": ReceiveBarCodeCount,
                                "barWidth": "auto",
                                "itemStyle": {
                                    "normal": {
                                        "color": {
                                            "type": "linear",
                                            "x": 0,
                                            "y": 0,
                                            "x2": 0,
                                            "y2": 1,
                                            "colorStops": [
                                                {
                                                    "offset": 0,
                                                    "color": "#609db8"
                                                },

                                                {
                                                    "offset": 1,
                                                    "color": "#609db8"
                                                }
                                            ],
                                            "globalCoord": false
                                        }
                                    }
                                }
                            },
                            {
                                "name": "完成量",
                                "type": "bar",
                                "data": TestBarCodeCount,
                                "barWidth": "auto",

                                "itemStyle": {
                                    "normal": {
                                        "color": {
                                            "type": "linear",
                                            "x": 0,
                                            "y": 0,
                                            "x2": 0,
                                            "y2": 1,
                                            "colorStops": [
                                                {
                                                    "offset": 0,
                                                    "color": "#66b8a7"
                                                },
                                                {
                                                    "offset": 1,
                                                    "color": "#66b8a7"
                                                }
                                            ],
                                            "globalCoord": false
                                        }
                                    }
                                },
                                "barGap": "0"
                            },
                            {
                                "name": "完成率",
                                "type": "line",
                                "yAxisIndex": 1,

                                "data": TestFinishRate,
                                lineStyle: {
                                    normal: {
                                        width: 2
                                    },
                                },
                                "itemStyle": {
                                    "normal": {
                                        "color": "#cdba00",

                                    }
                                },
                                "smooth": true
                            }
                        ]
                    };


                    // 使用刚指定的配置项和数据显示图表。
                    myChart.setOption(option);
                    window.addEventListener("resize", function () {
                        myChart.resize();
                    });
                }
            }
        });
        
    }
    //检验完成量统计
    function echarts_5() {
        var url1 = url + "?DataType=1&Limit=11";
       // url1 += "&StartDateTime=" + toString(getNextDate(new Date(), -30));
        url1 += "&EndDateTime=" + toString(new Date());
        $.ajax({
            url: url1,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    var ResultDataValue = JSON.parse(data.ResultDataValue);
                    console.log("检验完成量统计", ResultDataValue);
                    var LabName = [];
                    var ReceiveBarCodeCount = [];
                    var FinishBarCodeCount = [];
                    for (var i = 0; i < ResultDataValue.length; i++) {
                        LabName.push(ResultDataValue[i].LabName);
                        //LabName.push(ResultDataValue[i].LabName.substr(0, 5) + "..");
                        ReceiveBarCodeCount.push(ResultDataValue[i].ReceiveBarCodeCount);
                        if (ResultDataValue[i].ReceiveBarCodeCount==0) {
                            FinishBarCodeCount.push(0);
                        } else {
                            FinishBarCodeCount.push((Math.round(ResultDataValue[i].FinishBarCodeCount / ResultDataValue[i].ReceiveBarCodeCount * 10000 / 100)));
                        }
                        
                    }
                    LabName.reverse();
                    ReceiveBarCodeCount.reverse();
                    FinishBarCodeCount.reverse();
                    // 基于准备好的dom，初始化echarts实例
                    var myChart = echarts.init(document.getElementById('echart5'));
                    // 颜色
                    var lightBlue = {
                        type: 'linear',
                        x: 0,
                        y: 0,
                        x2: 0,
                        y2: 1,
                        colorStops: [{
                            offset: 0,
                            color: 'rgba(41, 121, 255, 1)'
                        }, {
                            offset: 1,
                            color: 'rgba(0, 192, 255, 1)'
                        }],
                        globalCoord: false
                    }

                    var option = {
                        tooltip: {
                            show: true,
                            formatter: function (params) {
                                return params.name;
                            }
                        },

                        grid: {
                            top: '0%',
                            left: '95',
                            right: '5%',
                            bottom: '0%',
                        },
                        xAxis: {
                            min: 0,
                            max: 100,
                            splitLine: {
                                show: false
                            },
                            axisTick: {
                                show: false
                            },
                            axisLine: {
                                show: false
                            },
                            axisLabel: {
                                show: false
                            }
                        },
                        yAxis: {
                            data: LabName,
                            //offset: 15,
                            axisTick: {
                                show: false
                            },
                            axisLine: {
                                show: false
                            },
                            axisLabel: {
                                color: 'rgba(255,255,255,.6)',
                                fontSize: 14,
                                formatter: function (params) {
                                    return params.substr(0, 5) + "..";
                                }
                                //,padding: [0, 0, 0, -80],
                                //align:"left"
                            }
                        },
                        series: [{
                            type: 'bar',
                            label: {
                                show: true,
                                zlevel: 10000,
                                position: 'right',
                                padding: 10,
                                color: '#49bcf7',
                                fontSize: 14,
                                formatter: '{c}%'

                            },
                            itemStyle: {
                                color: '#49bcf7'
                            },
                            barWidth: '15',
                            data: FinishBarCodeCount,
                            z: 10
                        }, {
                            type: 'bar',
                            barGap: '-100%',
                            itemStyle: {
                                color: '#fff',
                                opacity: 0.1
                            },
                            barWidth: '15',
                                data: ReceiveBarCodeCount,
                            z: 5
                        }],
                    };
                    // 使用刚指定的配置项和数据显示图表。
                    myChart.setOption(option);
                    window.addEventListener("resize", function () {
                        myChart.resize();
                    });
                }
            }
        });
        
    }

    //检验完成
    function zb() {
        var url1 = url + "?DataType=2&StartDateTime=" + toString(new Date()) + "&EndDateTime=" + toString(new Date()) + " 23:59:59";
        $.ajax({
            url: url1,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    var ResultDataValue = JSON.parse(data.ResultDataValue);
                    console.log("检验完成", ResultDataValue);
                    $("#allTestsample").text(ResultDataValue.BarCodeCount);
                    zb1(ResultDataValue.BarCodeCount - ResultDataValue.BarCodeTestFinishCount, ResultDataValue.BarCodeTestFinishCount);
                    zb2(ResultDataValue.BarCodeCount - ResultDataValue.BarCodeTestingCount, ResultDataValue.BarCodeTestingCount);
                    zb3(ResultDataValue.BarCodeCount - ResultDataValue.BarCodeUnTestCount , ResultDataValue.BarCodeUnTestCount);
                }
            }
        });
    };
    function zb1(v1,v2) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('zb1'));
        var v3 = v1 + v2//总消费 
        option = {
            series: [{

                type: 'pie',
                radius: ['60%', '70%'],
                color: '#49bcf7',
                label: {
                    normal: {
                        position: 'center'
                    }
                },
                data: [{
                    value: v2,
                    name: '女消费',
                    label: {
                        normal: {
                            formatter: v2 + '',
                            textStyle: {
                                fontSize: 20,
                                color: '#fff',
                            }
                        }
                    }
                }, {
                    value: v1,
                    name: '男消费',
                    label: {
                        normal: {
                            formatter: function (params) {
                                return '占比' + Math.round(v2 / v3 * 100) + '%'
                            },
                            textStyle: {
                                color: '#aaa',
                                fontSize: 12
                            }
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: 'rgba(255,255,255,.2)'
                        },
                        emphasis: {
                            color: '#fff'
                        }
                    },
                }]
            }]
        };
        myChart.setOption(option);
        window.addEventListener("resize", function () {
            myChart.resize();
        });
        
    }
    //检验中
    function zb2(v1, v2) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('zb2'));
        
        var v3 = v1 + v2//总消费 
        option = {

            //animation: false,
            series: [{
                type: 'pie',
                radius: ['60%', '70%'],
                color: '#cdba00',
                label: {
                    normal: {
                        position: 'center'
                    }
                },
                data: [{
                    value: v2,
                    name: '男消费',
                    label: {
                        normal: {
                            formatter: v2 + '',
                            textStyle: {
                                fontSize: 20,
                                color: '#fff',
                            }
                        }
                    }
                }, {
                    value: v1,
                    name: '女消费',
                    label: {
                        normal: {
                            formatter: function (params) {
                                return '占比' + Math.round(v2 / v3 * 100) + '%'
                            },
                            textStyle: {
                                color: '#aaa',
                                fontSize: 12
                            }
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: 'rgba(255,255,255,.2)'
                        },
                        emphasis: {
                            color: '#fff'
                        }
                    },
                }]
            }]
        };
        myChart.setOption(option);
        window.addEventListener("resize", function () {
            myChart.resize();
        });
    }
    //待检检验
    function zb3(v1, v2) {
        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('zb3'));
        
        var v3 = v1 + v2//总消费 
        option = {
            series: [{

                type: 'pie',
                radius: ['60%', '70%'],
                color: '#62c98d',
                label: {
                    normal: {
                        position: 'center'
                    }
                },
                data: [{
                    value: v2,
                    name: '女消费',
                    label: {
                        normal: {
                            formatter: v2 + '',
                            textStyle: {
                                fontSize: 20,
                                color: '#fff',
                            }
                        }
                    }
                }, {
                    value: v1,
                    name: '男消费',
                    label: {
                        normal: {
                            formatter: function (params) {
                                return '占比' + Math.round(v2 / v3 * 100) + '%'
                            },
                            textStyle: {
                                color: '#aaa',
                                fontSize: 12
                            }
                        }
                    },
                    itemStyle: {
                        normal: {
                            color: 'rgba(255,255,255,.2)'
                        },
                        emphasis: {
                            color: '#fff'
                        }
                    },
                }]
            }]
        };
        myChart.setOption(option);
        window.addEventListener("resize", function () {
            myChart.resize();
        });
    }
    //标本物流信息
    function SampleWLMsg() {
        var url1 = url + "?DataType=4&Limit=11";
        url1 += "&StartDateTime=" + toString(getNextDate(new Date(), -30));
        url1 += "&EndDateTime=" + toString(new Date());
        $.ajax({
            url: url1,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                $("#SampleWLMsgul").empty();
                if (data.success) {
                    var ResultDataValue = JSON.parse(data.ResultDataValue);
                    console.log("标本物流信息", ResultDataValue);
                    if (ResultDataValue && ResultDataValue.length) {
                        for (var i = 0; i < ResultDataValue.length; i++) {
                            $("#SampleWLMsgul").append("<li><p><span>" + ResultDataValue[i].LabName + "</span><span>" + ResultDataValue[i].DeliveryBarCodeCount + "</span><span>" + ResultDataValue[i].DateTime + "</span></p></li>");

                        }
                    }
                }
            }
        });
    };
    //检验量排名(总)
    function samplesortall() {
        var url1 = url + "?DataType=3&Limit=5";
        url1 += "&StartDateTime=" + toString(getNextDate(new Date(), -30));
        url1 += "&EndDateTime=" + toString(new Date());
        $.ajax({
            url: url1,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    var ResultDataValue = JSON.parse(data.ResultDataValue);
                    console.log("检验量排名(总)", ResultDataValue);
                    $("#samplesortalltable").empty();
                    for (var i = 0; i < ResultDataValue.length; i++) {
                        $("#samplesortalltable").append("<tr><td><span>" + (i + 1) + "</span></td><td>" + ResultDataValue[i].LabName + "</td><td>" + ResultDataValue[i].FinishBarCodeCount + "</td></tr > ");
                    }
                }
            }
        });
    };
    //送检量排名
    function sjSampleCountSort() {
        var url1 = url + "?DataType=6&Limit=5";
        url1 += "&StartDateTime=" + toString(getNextDate(new Date(), -30));
        url1 += "&EndDateTime=" + toString(new Date());
        $.ajax({
            url: url1,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    var ResultDataValue = JSON.parse(data.ResultDataValue);
                    console.log("送检量排名", ResultDataValue);
                    $("#sjSampleCountSorttable").empty();
                    for (var i = 0; i < ResultDataValue.length; i++) {
                        $("#sjSampleCountSorttable").append("<tr><td><span>" + (i + 1) + "</span></td><td>" + ResultDataValue[i].LabName + "</td><td>" + ResultDataValue[i].SendBarCodeCount + "</td></tr > ");
                    }
                }
            }
        });
    };
    init();
});


















