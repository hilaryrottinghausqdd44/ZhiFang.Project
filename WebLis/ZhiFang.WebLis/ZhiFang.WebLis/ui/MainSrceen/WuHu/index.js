$(window).load(function () {
    $(".loading").fadeOut()
})
$(function () {
    var href = window.document.location.href,
        pathname = window.document.location.pathname,
        pos = href.indexOf(pathname),
        localhostPaht = href.substring(0, pos),
        projectName = pathname.substring(0, pathname.substr(1).indexOf('/') + 1),
        selectUrl = localhostPaht + projectName + "/ServiceWCF/StatisticsService.svc/Wuhu_StatisticsMainScreen",
        SearchTime = "1800000", //当日查询时间间隔  毫秒为单位 默认半小时 通过selectUrl传值SearchTime分钟数也可以
        //用于判断是否执行滚动
        length = 2,
        count = 0,
        isLiMarquee = true;
    function init() {
        var Today = toString(new Date());//当日
        getParams();
        initYearSelect();
        SearchData();
        //区域当日检验量半小时刷新
        setInterval(function () {
            RegionalDailyInspectionQuantity(Today + " 00:00:00", Today+" 23:59:59", 7, null);
        }, SearchTime);
        //刷新页面
        refresh();
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
    //初始化年份下拉框
    function initYearSelect() {
        var years = [2019, 2020, 2021, 2022, 2023],
            nowYear = new Date().getFullYear(),
            optionHtml = "<option value=''>全部</option>";
        $.each(years, function (i, item) {
            optionHtml += "<option value='" + item + "'>" + item+"年</option>";
        });
        $("#year").html(optionHtml);
        $("#year").val(nowYear);
    }
    //根据年份选择获得时间段
    function getTimeSlotByYear() {
        var year = $("#year").val() || "";
        if (year == "")
            return "2019-01-01 - 2023-12-31"; 
        else 
            return year + "-01-01 - " + year+"-12-31"; 
    }
    //监听切换年份
    $("#year").change(function () {
        SearchData();
    });
    //查询数据
    function SearchData() {
        var time = getTimeSlotByYear();
        var StartDateTime = time.split(" - ")[0] +" 00:00:00";//开始时间
        var EndDateTime = time.split(" - ")[1] + " 23:59:59";//结束时间
        var Today = toString(new Date());//当日
        var yesterday = toString(getNextDate(new Date(), -1));//昨天
        var ThirtyDays = toString(getNextDate(new Date(), -30));//30天前
        //性别分布
        getData(StartDateTime, EndDateTime, 1, null, function (res) {
            if (res.success) {
                var ResultDataValue = JSON.parse(res.ResultDataValue);
                var xAxisData = ["男", "女"];
                var seriesData = [ResultDataValue["Man"], ResultDataValue["WuMan"]];
                ConeChart('echart1', xAxisData, seriesData);
            }
        });
        //年龄分布
        getData(StartDateTime, EndDateTime, 2, null, function (res) {
            if (res.success) {
                var ResultDataValue = JSON.parse(res.ResultDataValue);
                var xAxisData = ["5岁以下", "5-14岁", "15-44岁", "45-59岁", "60岁以上"];
                var seriesData = [ResultDataValue["Age5"], ResultDataValue["Age5_14"], ResultDataValue["Age15_44"], ResultDataValue["Age45_59"], ResultDataValue["Age60"]];
                ConeChart('echart2', xAxisData, seriesData);
            }
        });
        //检验数据构成
        getData(StartDateTime, EndDateTime, 3, null, function (res) {
            if (res.success) {
                var ResultDataValue = JSON.parse(res.ResultDataValue),
                    seriesData = [];
                $.each(ResultDataValue, function (i, item) {
                    seriesData.push({ value: item["clientpercentage"], name: item["clientzdy3"]});
                });
                RingDiagram('echart3', seriesData);
            }
        });
        //检验数据分析
        getData(StartDateTime, EndDateTime, 4, null, function (res) {
            if (res.success) {
                var ResultDataValue = JSON.parse(res.ResultDataValue),
                    NumberOfInspectorsHtml = '',
                    NumberOfTestSpecimensHtml = '';
                //检验人次数
                $("#NumberOfInspectors").html('');
                for (var i = 0; i < ResultDataValue["SickTypeCount"].length; i++) {
                    NumberOfInspectorsHtml += ('<span class="LCDNumBox"><image style="width: 20px;height: 40px;" src="../images/' + ResultDataValue["SickTypeCount"][i]+'.png" /></span >');
                }
                $("#NumberOfInspectors").html(NumberOfInspectorsHtml);
                //检验样本数
                $("#NumberOfTestSpecimens").html('');
                for (var i = 0; i < ResultDataValue["SampleCount"].length; i++) {
                    NumberOfTestSpecimensHtml += ('<span class="LCDNumBox"><image style="width: 20px;height: 40px;" src="../images/' + ResultDataValue["SampleCount"][i] +'-1.png" /></span>');
                }
                $("#NumberOfTestSpecimens").html(NumberOfTestSpecimensHtml);
                //饼图
                $.each(ResultDataValue["WHSickTypeSample"], function (a, itemA) {
                    if (itemA["Name"] == "门诊")
                        NumPie('zb2', itemA["Name"], itemA["Count"], itemA["ALLCount"]);
                    else if (itemA["Name"] == "住院")
                        NumPie('zb3', itemA["Name"], itemA["Count"], itemA["ALLCount"]);
                    else if (itemA["Name"] == "急诊")
                        NumPie('zb4', itemA["Name"], itemA["Count"], itemA["ALLCount"]);
                    else if (itemA["Name"] == "体检")
                        NumPie('zb5', itemA["Name"], itemA["Count"], itemA["ALLCount"]);
                });
                //折线图
                var xAxisData = [], SickTypeSeriesData = [], SampleSeriesData = [];
                $.each(ResultDataValue["WHSickTypeFigure"], function (b, itemB) {
                    if (xAxisData.indexOf(itemB["Times"]) == -1) xAxisData.push(itemB["Times"]);
                    SickTypeSeriesData.push(itemB["Count"]);
                });
                $.each(ResultDataValue["WHSampleFigure"], function (c, itemC) {
                    if (xAxisData.indexOf(itemC["Times"]) == -1) xAxisData.push(itemC["Times"]);
                    SampleSeriesData.push(itemC["Count"]);
                });
                NumLine('echart4', xAxisData, SickTypeSeriesData, SampleSeriesData);
            }
        });
        //医院分布
        getData(StartDateTime, EndDateTime, 5, null, function (res) {
            if (res.success) {
                var ResultDataValue = JSON.parse(res.ResultDataValue),
                    seriesData = [];
                $.each(ResultDataValue, function (i, item) {
                    seriesData.push({ value: item["clientpercentage"], name: item["czdy1"] });
                });
                HospitalDistribution('echart5', seriesData);
            }
        });
        //城乡分布
        getData(StartDateTime, EndDateTime, 6, null, function (res) {
            if (res.success) {
                var ResultDataValue = JSON.parse(res.ResultDataValue),
                    data = {};
                $.each(ResultDataValue, function (i, item) {
                    if (item["czdy2"] == null) return true;
                    if (item["czdy2"].indexOf("市") != -1 && !data["city"])
                        data["city"] = { value: item["clientpercentage"], name: item["czdy2"] };
                    else 
                        data["county"] = { value: item["clientpercentage"], name: item["czdy2"] };
                });
                UrbanAndRuralDistribution('echart6', data);
            }
        });
        //区域当日检验量
        RegionalDailyInspectionQuantity(Today + " 00:00:00", Today + " 23:59:59", 7, null);
        //区域近30日检验量
        getData(ThirtyDays + " 00:00:00", yesterday+" 23:59:59", 7, null, function (res) {
            count++;
            if (res.success) {
                var ResultDataValue = JSON.parse(res.ResultDataValue),
                    html = '';
                $("#SampleWLMsgul2").html("");
                $.each(ResultDataValue, function (i, item) {
                    html += '<li><p><span>' + item["CLIENTNAME"] + '</span><span><b class="line" style="width:' + item["clientpercentage"] + '%;"><i class="circle"></i><b class="lineValue">' + item["clientcount"] + '</b></b></span></p></li>';
                });
                $("#SampleWLMsgul2").html(html);
            }
            if (length == count && isLiMarquee) {
                liMarquee();
                isLiMarquee = false;
            }
        });
        //人均检验费用
        getData(StartDateTime, EndDateTime, 9, null, function (res) {
            if (res.success) {
                var ResultDataValue = JSON.parse(res.ResultDataValue),
                    xAxisData = [],
                    seriesData = [];
                $.each(ResultDataValue, function (i, item) {
                    xAxisData.push(item["Times"]);
                    seriesData.push([item["Times"], item["avgparse"] == null ? 0 : item["avgparse"]]);
                });
                PerCapitaCost('echart7', xAxisData, seriesData);
            }
        });
    };
    //区域当日检验量
    function RegionalDailyInspectionQuantity(StartDateTime, EndDateTime,DataType,limit) {
        getData(StartDateTime, EndDateTime, DataType, null, function (res) {
            count++;
            if (res.success) {
                var ResultDataValue = JSON.parse(res.ResultDataValue),
                    html = '';
                $("#SampleWLMsgul").html("");
                $.each(ResultDataValue, function (i, item) {
                    html += '<li><p><span>' + item["CLIENTNAME"] + '</span><span><b class="line" style="width:' + item["clientpercentage"] + '%;"><i class="circle"></i><b class="lineValue">' + item["clientcount"] + '</b></b></span></p></li>';
                });
                $("#SampleWLMsgul").html(html);
            }
            if (length == count && isLiMarquee) {
                liMarquee();
                isLiMarquee = false;
            }
        });
    }
    //锥形图
    function ConeChart(elemID, xAxisData, seriesData) {
        var myChart = echarts.init(document.getElementById(elemID));
        var option = {
            //backgroundColor: "#38445E",
            grid: {
                left: '5%',
                top: '5%',
                bottom: '15%',
                right: '8%'
            },
            xAxis: {
                data: xAxisData,//['驯鹿', '火箭', '飞机', '高铁'],
                axisTick: {
                    show: false
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255, 129, 109, 0.1)',
                        width: 1 //这里是为了突出显示加上的
                    }
                },
                axisLabel: {
                    textStyle: {
                        color: '#22D0DD',
                        fontSize: 10
                    }
                }
            },
            yAxis: [{
                min: 0,
                max:120,
                show: false,
                splitNumber: 2,
                axisTick: {
                    show: false
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255, 129, 109, 0.1)',
                        width: 1 //这里是为了突出显示加上的
                    }
                },
                axisLabel: {
                    textStyle: {
                        color: '#999'
                    }
                },
                splitArea: {
                    areaStyle: {
                        color: 'rgba(255,255,255,.5)'
                    }
                },
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: 'rgba(255, 129, 109, 0.1)',
                        width: 0.5,
                        type: 'dashed'
                    }
                }
            }
            ],
            series: [{
                name: 'Gender',
                type: 'pictorialBar',
                barCategoryGap: '0%',
                symbol: 'path://M0,10 L10,10 C5.5,10 5.5,5 5,0 C4.5,5 4.5,10 0,10 z',
                label: {
                    show: true,
                    position: 'top',
                    distance: 15,
                    color: '#fff',
                    fontWeight: 'bolder',
                    fontSize: 14,
                    formatter: '{c}%'
                },
                itemStyle: {
                    normal: {
                        color: {
                            type: 'linear',
                            x: 0,
                            y: 0,
                            x2: 0,
                            y2: 1,
                            colorStops: [{
                                offset: 0,
                                color: 'rgba(202,152,76,.8)' //  0%  处的颜色
                            },
                            {
                                offset: 1,
                                color: 'rgba(21,209,221, .1)' //  100%  处的颜色
                            }
                            ],
                            global: false //  缺省为  false
                        }
                    },
                    emphasis: {
                        opacity: 1
                    }
                },
                data: seriesData,//[123, 60, 25, 18],
                z: 10
            }]
        };
        myChart.setOption(option); // 使用刚指定的配置项和数据显示图表。
    }
    //环形图
    function RingDiagram(elemID, seriesData) {
        var myChart = echarts.init(document.getElementById(elemID));
        var option = {
            title: {
                show: false,
                text: 'Customized Pie',
                left: 'center',
                top: 20,
                textStyle: {
                    color: '#ccc'
                }
            },
            color: ['#69C4A1', '#98ECF7', '#B9E2F4', '#F38128', '#F0D92B', '#305B9B', '#ca8622', '#bda29a', '#6e7074', '#546570', '#c4ccd3'],
            tooltip: {
                trigger: 'item',
                formatter: '{a} <br/>{b} : {c} ({d}%)'
            },
            //visualMap: {
            //    show: false,
            //    min: 80,
            //    max: 600,
            //    inRange: {
            //        colorLightness: [0, 1]
            //    }
            //},
            series: [
                {
                    name: '',
                    type: 'pie',
                    radius: [30, 90],//'55%',
                    center: ['50%', '50%'],
                    //data: [
                    //    { value: 335, name: '直接访问' },
                    //    { value: 310, name: '邮件营销' },
                    //    { value: 274, name: '联盟广告' },
                    //    { value: 235, name: '视频广告' },
                    //    { value: 400, name: '搜索引擎' }
                    //].sort(function (a, b) { return a.value - b.value; }),
                    data: seriesData.sort(function (a, b) { return a.value - b.value; }),
                    roseType: 'radius',
                    label: {
                        show:true,
                        formatter: '{b} {c}%'//,
                        //color: 'rgba(255, 255, 255, 0.3)'
                    },
                    labelLine: {
                        //lineStyle: {
                        //    color: 'rgba(255, 255, 255, 0.3)'
                        //},
                        smooth: 0.2,
                        length: 5,
                        length2: 10
                    },
                    itemStyle: {
                        //color: '#c23531',
                        //shadowBlur: 10,
                        //shadowColor: 'rgba(0, 0, 0, 0.5)'
                    },
                    animationType: 'scale',
                    animationEasing: 'elasticOut',
                    animationDelay: function (idx) {
                        return Math.random() * 200;
                    }
                }
            ]
        };
        myChart.setOption(option); // 使用刚指定的配置项和数据显示图表。
    }
    //标本数饼图
    function NumPie(elemID,name, val, count) {
        var myChart = echarts.init(document.getElementById(elemID));
        var option = {
            series: [
                {
                    name: '访问来源',
                    type: 'pie',
                    radius: ['90%', '75%'],
                    avoidLabelOverlap: false,
                    label: {
                        show: false,
                        position: 'center'
                    },
                    labelLine: {
                        show: false
                    },
                    data: [
                        {
                            value: val, name: name,
                            label: {
                                normal: {
                                    show: true,
                                    position: 'center',
                                    formatter: function (data) {
                                        return '{a|' + data.value + '}\n\n{b|' + data.name + '标本数}';
                                    },
                                    rich: {
                                        a: {
                                            color: '#fff',
                                            //fontFamily: 'Microsoft YaHei',
                                            fontSize: 18,
                                            fontWeight:700
                                        },
                                        b: {
                                            color: '#45b9ca',
                                            fontFamily: 'Microsoft YaHei',
                                            fontSize: 13
                                        }
                                    }
                                }
                            },
                            itemStyle: {
                                color: {
                                    type: 'linear',
                                    x: 0,
                                    y: 0,
                                    x2: 0,
                                    y2: 1,
                                    colorStops: [{
                                        offset: 0, color: '#34EBF0' // 0% 处的颜色
                                    }, {
                                        offset: 1, color: '#D7903E' // 100% 处的颜色
                                    }],
                                    global: false // 缺省为 false
                                }
                            }
                        },
                        { value: count - val, name: '底色', itemStyle: { color: 'rgba(23,49,127,1)' }, silent: true },
                    ]
                }
            ]
        };
        myChart.setOption(option); // 使用刚指定的配置项和数据显示图表。
    }
    //检验数据标本数折线图
    function NumLine(elemID, xAxisData, SickTypeSeriesData, SampleSeriesData) {
        var xAxisData = xAxisData || [],
            SickTypeSeriesData = SickTypeSeriesData || [],
            SampleSeriesData = SampleSeriesData || [];
        var myChart = echarts.init(document.getElementById(elemID));
        var option = {
            title: {
                show:false,
                text: '折线图堆叠',
                textStyle: {
                    color: '#22D0DD'
                }
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                right: 50,
                textStyle: {
                    color: '#22D0DD'
                }
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '10%',
                containLabel: true
            },
            xAxis: {
                type: 'category',
                boundaryGap: true,
                data: xAxisData,
                axisLine: {
                    onZero: false,
                    lineStyle: {
                        color: '#22D0DD'
                    }
                }
            },
            yAxis: {
                type: 'value',
                axisLine: {
                    onZero: false,
                    lineStyle: {
                        color: '#22D0DD'
                    }
                }
            },
            color: ['#0D9CAD', '#A57756'],
            series: [
                {
                    symbol: 'circle',
                    symbolSize: 8,
                    name: '就诊人数',
                    type: 'line',
                    stack: '总量',
                    data: SickTypeSeriesData,
                    label: {
                        show:true
                    }
                },
                {
                    symbolSize: 8,
                    symbol: 'circle',
                    smooth: true,
                    name: '标本数',
                    type: 'line',
                    stack: '总量',
                    data: SampleSeriesData,
                    label: {
                        show: true
                    }
                }
            ]
        };
        myChart.setOption(option); // 使用刚指定的配置项和数据显示图表。
    };
    //城乡分布饼图
    function UrbanAndRuralDistribution(elemID, data) {
        var myChart = echarts.init(document.getElementById(elemID));

        var data = data || {};//{ city: { value: 80, name: "城市医院" }, county: { value: 20, name: "县级医院" } };
        if (!data["city"] || !data["county"]) return;
        var option = {
            "series": [{
                name: '',
                type: 'pie',
                radius: ['65%', '65%'],
                data: [{ value: 100 }],
                label: { show: false },
                itemStyle: {
                    color: 'red',
                    borderColor: '#05A4F2',
                    borderWidth: '2',
                    borderType: 'dashed'
                },
                silent: true
            }, {
                type: 'pie',
                "center": ["50%", "50%"],
                "radius": ["20%", "40%"],
                "hoverAnimation": false,
                startAngle: -180,
                clockwise: false,
                labelLine: {
                    show: false
                },
                "data": [{
                    "name": data["city"]["name"],
                    "value": data["city"]["value"] > 100 ? 100 : data["city"]["value"],
                    "label": {
                        "show": false,
                        "position": "center",
                        "formatter": function (o) {
                            return ['{a|' + data["city"]["value"] + '}{b|%}',
                                '{c|完成比}'
                            ].join('\n')
                        },
                        rich: {
                            a: {
                                color: '#5841F3',
                                fontSize: 16,
                                // fontWeight: 'bold',
                                fontFamily: 'mission'
                            },
                            b: {
                                color: '#5841F3',
                                fontSize: 12,
                                // fontWeight: 'bold'
                            },
                            c: {
                                color: '#263039',
                                fontSize: 12
                            }
                        }
                    },
                    itemStyle: {
                        color: {
                            type: 'linear',
                            x: 0,
                            y: 0,
                            x2: 0,
                            y2: 1,
                            colorStops: [{
                                offset: 0, color: '#D9FB28' // 0% 处的颜色
                            }, {
                                offset: 1, color: '#68D0DF' // 100% 处的颜色
                            }],
                            global: false // 缺省为 false
                        }
                    },

                },
                { //画中间的图标
                    "name": "",
                    "value": 0,
                    itemStyle: {
                        color: 'rgb(0 2 69)'
                    },
                },
                { //画剩余的刻度圆环
                    "name": "",
                    "value": 100 - (data > 100 ? 100 : data),
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                    "label": {
                        show: false
                    }
                }
                ]
            },
            {
                type: 'pie',
                "center": ["50%", "50%"],
                "radius": ["20%", "55%"],
                "hoverAnimation": false,
                startAngle: -180,
                clockwise: false,
                labelLine: {
                    show: false
                },
                itemStyle: {
                    color: 'rgba(0,0,0,0)'
                },
                emphasis: {
                    label: {
                        // color: "rgb(0 2 69);" ,
                        borderColor: 'rgb(0 2 69)'

                    }
                },
                data: [{
                    name: data["city"]["name"],
                    value: data["city"]["value"] / 2,
                },
                { //画中间的图标
                    "name": data["city"]["value"],
                    "value": 0,
                    itemStyle: {
                        color: '#000'
                    },
                    "label": {
                        position: 'inside',
                        formatter: function () {
                            //return '{a|城}'
                            return '{a|' + data["city"]["name"].substr(0, 1) + '}'
                        },
                        rich: {
                            a: {
                                color: '#000',
                                fontSize: 16,
                                width: 42,
                                height: 42,
                                borderRadius: 21,
                                borderWidth: 4,
                                borderColor: '#000',
                                fontWeight: 70,
                                // lineHeight:100,
                                backgroundColor: '#D5FF6E',
                            }
                        }
                    },

                },
                { //画剩余的刻度圆环
                    "name": "",
                    "value": 100 - data["city"]["value"] / 2,
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                    "label": {
                        show: false
                    }
                }
                ]
            },
            //支de 半圆的线
            {
                type: 'pie',
                "center": ["50%", "50%"],
                "radius": ["15%", "45%"],
                "hoverAnimation": false,
                startAngle: -180,
                clockwise: false,
                labelLine: {
                    show: false
                },
                data: [{
                    name: '',
                    value: data["city"]["value"] * 1,
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                },
                {
                    name: data["county"]["name"],
                    value: data["county"]["value"],
                    itemStyle: {
                        color: {
                            type: 'linear',
                            x: 0,
                            y: 0,
                            x2: 0,
                            y2: 1,
                            colorStops: [{
                                offset: 0, color: '#41BCB5' // 0% 处的颜色
                            }, {
                                offset: 1, color: '#2CB7E2' // 100% 处的颜色
                            }],
                            global: false // 缺省为 false
                        }
                    },
                    label: { show: false }
                },
                { //画中间的图标
                    "name": "",
                    "value": 0,
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                }
                ]
            },
            //支的中心圆
            {
                type: 'pie',
                "center": ["50%", "50%"],
                "radius": ["40%", "50%"],
                "hoverAnimation": false,
                startAngle: -180,
                clockwise: false,
                labelLine: {
                    show: false
                },
                data: [{
                    name: '',
                    value: data["city"]["value"],
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                },
                {
                    name: '',
                    value: data["county"]["value"] * .5,
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                },
                { //画中间的图标
                    "name": '',
                    "value": 0,
                    itemStyle: {
                        color: '#000'
                    },
                    "label": {
                        position: 'inside',
                        fontWeight: 'normal',
                        formatter: function () {
                            //return '{a|县}'
                            return '{a|' + data["county"]["name"].substr(0, 1) + '}'
                        },
                        rich: {
                            a: {
                                color: '#000',
                                fontSize: 16,
                                width: 42,
                                height: 42,
                                borderRadius: 21,
                                fontWeight: 70,
                                borderWidth: 4,
                                borderColor: '#000',
                                fontFamily: 'Microsoft YaHei',
                                // lineHeight:100,
                                backgroundColor: '#4BA99D',
                            }
                        }
                    }
                },
                { //画剩余的刻度圆环
                    "name": "",
                    "value": data["county"]["value"] - data["county"]["value"] * .5,
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                    "label": {
                        show: false
                    }
                }
                ]
            },
            //支的label线
            {
                type: 'pie',
                "center": ["50%", "50%"],
                "radius": ["50%", "50%"],
                "hoverAnimation": false,
                startAngle: -180,
                clockwise: false,
                labelLine: {
                    show: false
                },
                data: [{
                    name: '',
                    value: data["city"]["value"],
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                },
                {
                    name: '',
                    value: data["county"]["value"] * .5,
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                },
                { //画中间的图标
                    "name": "",
                    "value": 0,
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                    "label": {
                        show: true,
                        // position: 'inside',
                        fontWeight: 'normal',
                        fontSize: '14',
                        color: '#1ECADB',
                        formatter: function () {
                            return data["county"]["name"] + '\n' + data["county"]["value"] + '%'
                        }
                    },
                    labelLine: {
                        show: true,
                        length: 10,
                        length2: 10,
                        lineStyle: {
                            color: '#1ECADB'
                        }
                    }
                },
                { //画剩余的刻度圆环
                    "name": "",
                    "value": data["county"]["value"] - data["county"]["value"] * .5,
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                    "label": {
                        show: false
                    }
                }
                ]
            },
            //干的label线
            {
                type: 'pie',
                "center": ["50%", "50%"],
                "radius": ["50%", "50%"],
                "hoverAnimation": false,
                startAngle: -180,
                clockwise: false,
                labelLine: {
                    show: false
                },
                itemStyle: {
                    color: 'rgba(0,0,0,0)'
                },
                emphasis: {
                    label: {
                        // color: "rgb(0 2 69);" ,
                        borderColor: 'rgb(0 2 69)'

                    }
                },
                data: [{
                    name: '',
                    value: data["city"]["value"] / 2,
                },
                { //画中间的图标
                    "name": "",
                    "value": 0,
                    itemStyle: {
                        color: '#fff'
                    },
                    "label": {
                        // position: 'inside',
                        fontWeight: 'normal',
                        fontSize: '14',
                        color: '#1ECADB',
                        formatter: function () {
                            return data["city"]["name"] + '\n' + data["city"]["value"] + '%'
                        }
                    },
                    labelLine: {
                        show: true,
                        length: 10,
                        length2: 20,
                        lineStyle: {
                            color: '#D6F863'
                        }
                    }

                },
                { //画剩余的刻度圆环
                    "name": "",
                    "value": 100 - data["city"]["value"] / 2,
                    itemStyle: {
                        color: 'rgba(0,0,0,0)'
                    },
                    "label": {
                        show: false
                    }
                }
                ]
            }
            ]
        };
        myChart.setOption(option); // 使用刚指定的配置项和数据显示图表。
    }
    //人均检验费用折线图
    function PerCapitaCost(elemID, xAxisData, seriesData) {
        var myChart = echarts.init(document.getElementById(elemID));
        //var data = {
        //    "chart": [{
        //        month: "1月",
        //        value: 138,
        //        ratio: 14.89
        //    },

        //    {
        //        month: "2月",
        //        value: 114,
        //        ratio: 79.49
        //    },

        //    {
        //        month: "3月",
        //        value: 714,
        //        ratio: 75.8
        //    },

        //    {
        //        month: "4月",
        //        value: 442,
        //        ratio: 19.8
        //    }

        //    ]
        //};
        //var xAxisMonth = [],
        //    barData = [],
        //    barData2 = [],
        //    lineData = [];
        //for (var i = 0; i < data.chart.length; i++) {
        //    xAxisMonth.push(data.chart[i].month);
        //    barData.push({
        //        "name": xAxisMonth[i],
        //        "value": data.chart[i].value,
        //        "symbolOffset": [0, 0]
        //    });
        //    barData2.push({
        //        "name": xAxisMonth[i],
        //        "value": data.chart[i].value - 50,
        //        "symbolOffset": [10, 0]
        //    });
        //    lineData.push({
        //        "name": xAxisMonth[i],
        //        "value": data.chart[i].ratio
        //    });
        //}

        var option = {
            title: '',
            grid: {
                top: '10%',
                left: '7%',
                bottom: '6%',
                containLabel: true
            },
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'none'
                }
            },
            xAxis: [
                {
                    type: 'category',
                    show: true,
                    data: xAxisData,
                    axisLabel: {
                        textStyle: {
                            color: '#22D0DD'
                        }
                    },
                    axisTick: {
                        show: false
                    },
                    axisLine: {
                        show:false,
                        onZero: false,
                        lineStyle: {
                            color: '#22D0DD'
                        }
                    }
                }
                //,{
                //    type: 'category',
                //    position: "bottom",
                //    // data: xAxisMonth,
                //    boundaryGap: true,
                //    // offset: 40,
                //    axisTick: {
                //        show: false
                //    },
                //    axisLine: {
                //        show: false
                //    },
                //    axisLabel: {
                //        textStyle: {
                //            color: '#22D0DD'
                //        }
                //    }
                //}
            ],
            yAxis: [{
                show: true,
                name:'单位：元',
                splitLine: {
                    show: false,
                    lineStyle: {
                        color: "rgba(255,255,255,0.2)"
                    }
                },
                axisTick: {
                    show:false
                },
                axisLine: {
                    show: false,
                    onZero: false,
                    lineStyle: {
                        color: '#22D0DD'
                    }
                },
                axisLabel: {
                    show: true,
                    color: '#22D0DD'
                }
            }],
            color: ['#A7F5F9'],
            series: [
            //    {
            //    name: '训练人次',
            //    type: 'pictorialBar',
            //    xAxisIndex: 1,
            //    barWidth: 2,
            //    symbol: 'path://d="M150 130 L130 50 L170 50 Z"',
            //    itemStyle: {
            //        color: {
            //            type: 'linear',
            //            x: 0,
            //            y: 0,
            //            x2: 0,
            //            y2: 1,
            //            colorStops: [{
            //                offset: 0, color: 'rgba(14,171,190,0.7)' // 0% 处的颜色
            //            }, {
            //                offset: 1, color: 'rgba(14,171,190,0)' // 100% 处的颜色
            //            }],
            //            global: false // 缺省为 false
            //        },

            //        emphasis: {
            //            opacity: 0.5
            //        }
            //    },
            //    data: barData,
            //},
            //{
            //    symbol: 'circle',
            //    symbolSize: 10,
            //    symbolOffset: [0, '-50%'],
            //    symbolPosition: 'end',
            //    name: "完成率",
            //    itemStyle: {
            //        shadowColor: '#A7F5F9',
            //        shadowBlur: 10

            //    },
            //    type: "pictorialBar",
            //    xAxisIndex: 0,
            //    data: barData
            //}, {
            //    name: '训练人次',
            //    type: 'pictorialBar',
            //    xAxisIndex: 1,
            //    barWidth: 2,
            //    symbol: 'path://d="M150 130 L130 50 L170 50 Z"',
            //    itemStyle: {
            //        color: {
            //            type: 'linear',
            //            x: 0,
            //            y: 0,
            //            x2: 0,
            //            y2: 1,
            //            colorStops: [{
            //                offset: 0, color: 'rgba(251,145,0,1)' // 0% 处的颜色
            //            }, {
            //                offset: 1, color: 'rgba(14,171,190,0)' // 100% 处的颜色
            //            }],
            //            global: false // 缺省为 false
            //        },
            //        emphasis: {
            //            opacity: 0.5
            //        }
            //    },
            //    data: barData2,
            //},
            //{
            //    symbol: 'circle',
            //    symbolSize: 10,
            //    symbolOffset: [0, '-50%'],
            //    symbolPosition: 'end',
            //    name: "完成率",
            //    itemStyle: {
            //        shadowColor: 'rgba(251,145,0,1)',
            //        shadowBlur: 10,
            //        color: 'rgba(251,145,0,1)'
            //    },
            //    type: "pictorialBar",
            //    xAxisIndex: 0,
            //    data: barData2
            //}, 
            {
                symbolSize: 8,
                symbol: 'none',
                smooth: true,
                name: '人均检验费用',
                type: 'line',
                stack: '总量',
                areaStyle: {
                    color: {
                        type: 'linear',
                        x: 0,
                        y: 0,
                        x2: 0,
                        y2: 1,
                        colorStops: [{
                            offset: 0, color: 'rgba(0, 180, 0, 0.8)',
                            shadowColor: 'rgba(0, 180, 0, 1)',
                            shadowOffsetY:5,
                            shadowBlur: 5 // 0% 处的颜色
                        }, {
                            offset: 1, color: 'rgba(0, 180, 0, 0)' // 100% 处的颜色
                        }],
                        global: false // 缺省为 false
                    }
                    },
                    itemStyle: {
                        color: 'rgba(30,194,71, 1)',
                        shadowColor: 'rgba(0, 180, 90, 0.8)',
                        shadowBlur: 5
                        
                    },
                data: seriesData
            }
            ]
        };
        myChart.setOption(option); // 使用刚指定的配置项和数据显示图表。
    };
    //医院分布饼图
    function HospitalDistribution(elemID, seriesData) {
        var myChart = echarts.init(document.getElementById(elemID)),
            seriesData = seriesData || [];
        var option = {
            color: [{
                type: 'linear',
                x: 0,
                y: 0,
                x2: 0,
                y2: 1,
                colorStops: [{
                    offset: 0, color: '#2EB2FA' // 0% 处的颜色
                }, {
                    offset: 1, color: '#43CC31' // 100% 处的颜色
                }],
                global: false // 缺省为 false
            },{
                type: 'linear',
                x: 0,
                y: 0,
                x2: 0,
                y2: 1,
                colorStops: [{
                    offset: 0, color: '#FFC145' // 0% 处的颜色
                }, {
                    offset: 1, color: '#61a0a8' // 100% 处的颜色
                }],
                global: false // 缺省为 false
            },{
                type: 'linear',
                x: 0,
                y: 0,
                x2: 0,
                y2: 1,
                colorStops: [{
                    offset: 0, color: '#D9FB28' // 0% 处的颜色
                }, {
                    offset: 1, color: '#68D0DF' // 100% 处的颜色
                }],
                global: false // 缺省为 false
            },{
                type: 'linear',
                x: 0,
                y: 0,
                x2: 0,
                y2: 1,
                colorStops: [{
                    offset: 0, color: '#d48265' // 0% 处的颜色
                }, {
                    offset: 1, color: '#91c7ae' // 100% 处的颜色
                }],
                global: false // 缺省为 false
            },{
                type: 'linear',
                x: 0,
                y: 0,
                x2: 0,
                y2: 1,
                colorStops: [{
                    offset: 0, color: '#749f83' // 0% 处的颜色
                }, {
                    offset: 1, color: '#ca8622' // 100% 处的颜色
                }],
                global: false // 缺省为 false
            },{
                type: 'linear',
                x: 0,
                y: 0,
                x2: 0,
                y2: 1,
                colorStops: [{
                    offset: 0, color: '#bda29a' // 0% 处的颜色
                }, {
                    offset: 1, color: '#6e7074' // 100% 处的颜色
                }],
                global: false // 缺省为 false
            }, '#d48265', '#91c7ae', '#749f83', '#ca8622', '#bda29a', '#6e7074', '#546570', '#c4ccd3', '#c23531', '#2f4554'],
            title: {
                show:false,
                text: 'Customized Pie',
                left: 'center',
                top: 20,
                textStyle: {
                    color: 'red'
                }
            },
            tooltip: {
                trigger: 'item',
                formatter: '{a} <br/>{b} : {c} ({d}%)'
            },
            series: [
                {
                    name: '',
                    type: 'pie',
                    //radius: '55%',
                    radius: [10, 60],
                    center: ['50%', '50%'],
                    //data: [
                    //    { value: 335, name: '直接访问' },
                    //    { value: 310, name: '邮件营销' },
                    //    { value: 274, name: '联盟广告' },
                    //    { value: 235, name: '视频广告' },
                    //    { value: 400, name: '搜索引擎' }
                    //].sort(function (a, b) { return a.value - b.value; }),
                    data: seriesData.sort(function (a, b) { return a.value - b.value; }),
                    roseType: 'radius',
                    label: {
                        color: '#fff',
                        formatter: '         {c}% \n{b}'
                    },
                    labelLine: {
                        lineStyle: {
                            //color: 'red'
                        },
                        smooth: 0.2,
                        length: 10,
                        length2: 20
                    },
                    itemStyle: {
                        // color: '#c23531',
                        shadowBlur: 50,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    },

                    animationType: 'scale',
                    animationEasing: 'elasticOut',
                    animationDelay: function (idx) {
                        return Math.random() * 200;
                    }
                },
                {
                    name: '',
                    type: 'pie',
                    radius: [100, 0],
                    center: ['50%', '50%'],
                    data: [{ value: 100 }],
                    silent: true,
                    itemStyle: {
                        color: 'rgba(40,180,232,0.2)',
                        shadowBlur: 200,
                        shadowColor: 'rgba(0, 0, 0, 0.1)'
                    },
                    labelLine: {
                        normal: {
                            show: false
                        }
                    }
                },
                {
                    name: '',
                    type: 'pie',
                    radius: [130, 100],
                    center: ['50%', '50%'],
                    data: [{ value: 100 }],
                    silent: true,
                    itemStyle: {
                        color: 'rgba(40,180,232,0.1)',
                        shadowBlur: 10,
                        shadowColor: 'rgba(0, 0, 0, 0.05)'
                    },
                    labelLine: {
                        normal: {
                            show: false
                        }
                    }
                },
                {
                    name: '',
                    type: 'pie',
                    radius: [152, 150],
                    center: ['50%', '50%'],
                    data: [{ value: 100 }],
                    silent:true,
                    itemStyle: {
                        color: 'rgba(40,180,232,0.3)',
                        shadowBlur: 10,
                        shadowColor: 'rgba(69,185,202,1)'
                    },
                    labelLine: {
                        normal: {
                            show: false
                        }
                    }
                }
            ]
        };
        myChart.setOption(option); // 使用刚指定的配置项和数据显示图表。
    }
    //调用服务
    function getData(StartDateTime, EndDateTime, DataType, Limit, CallBack) {
        var url = selectUrl;
        if (StartDateTime) url += "?StartDateTime=" + StartDateTime;
        if (EndDateTime) url += "&EndDateTime=" + EndDateTime;
        if (DataType) url += "&DataType=" + DataType;
        if (Limit) url += "&Limit=" + Limit;
        url += "&time=" + new Date().getTime();
        $.ajax({
            url: url,
            dataType: 'json',
            type: 'GET',
            async: true,
            contentType: 'application/json',//不加这个会出现错误
            success: function (res) {
                if (typeof CallBack == "function") CallBack(res);
            }
        });
    }
    //页面滚动
    function liMarquee() {
        $('.wrap,.adduser').liMarquee({
            direction: 'up',/*身上滚动*/
            runshort: true,/*内容不足时不滚动*/
            scrollamount: 20/*速度*/
        });
    }
    //凌晨00点刷新页面
    function refresh() {
        var now = new Date().getTime(),//当前时间
            tomorrow = toString(getNextDate(new Date(), 1)) + " 00:00:00",//昨天
            times = new Date(tomorrow.replace(/-/g, '/')).getTime() - now;
        setTimeout(function () {
            history.go(0);
        }, times);
        //var days = parseInt(times / (1000 * 60 * 60 * 24));
        //var hours = parseInt((times % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        //var minutes = parseInt((times % (1000 * 60 * 60)) / (1000 * 60));
        //var seconds = (times % (1000 * 60)) / 1000;
        //console.log(days + " 天 " + hours + " 小时 " + minutes + " 分钟 " + seconds + " 秒 ");
    }
    init();
});