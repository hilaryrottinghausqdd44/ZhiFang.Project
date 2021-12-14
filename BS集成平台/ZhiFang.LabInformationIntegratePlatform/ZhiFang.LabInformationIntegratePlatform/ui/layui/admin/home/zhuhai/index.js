layui.extend({
    uxutil: 'ux/util'
}).use(['table', 'form', 'element', 'layer', 'uxutil'], function () {
    var $ = layui.jquery,
        table = layui.table,
        form = layui.form,
        layer = layui.layer,
        element = layui.element,
        uxutil = layui.uxutil;

    //当前登录实验室
    var EMPID = uxutil.cookie.get('USERID') || "";
    //当日报告数服务地址
    var GETDAYREPORTCOUNTURL = uxutil.path.LOCAL + '/ZFLIIP/ZhiFang.WebLis.PlatForm/ServerWCF/StatisticsService.svc/GetDayReportCount';
    //标本拒签数服务地址
    var GETDCSAMPLEREJECTINFOCOUNTURL = uxutil.path.LOCAL + '/ZFLIIP/ZhiFang.WebLis.PlatForm/ServerWCF/StatisticsService.svc/GetDCSampleRejectInfoCount';
    //当月危急值服务地址
    var GETMONTHCVCOUNTURL = uxutil.path.LOCAL + '/ZFLIIP/ZhiFang.WebLis.PlatForm/ServerWCF/StatisticsService.svc/GetMonthCVInfoListCount';
    //当月报告回传服务地址
    var GETMONTHREPORTFORMREBACKCOUNTURL = uxutil.path.LOCAL + '/ZFLIIP/ZhiFang.WebLis.PlatForm/ServerWCF/StatisticsService.svc/GetMonthReportFormReBackCount';
    //当月报告项目TOP10服务地址
    var GETREPORTITEMTOP10COUNTURL = uxutil.path.LOCAL + '/ZFLIIP/ZhiFang.WebLis.PlatForm/ServerWCF/StatisticsService.svc/GetReportItemTOP10Count';
    //当月报告项目量占比TOP10服务地址
    var GETREPORTITEMTOP10COUNTPROPORTIONURL = uxutil.path.LOCAL + '/ZFLIIP/ZhiFang.WebLis.PlatForm/ServerWCF/StatisticsService.svc/GetReportItemTOP10CountProportion';
    //结果回报指标分析服务地址
    var GETRESULTRETURNANINDEXALYSISURL = uxutil.path.LOCAL + '/ZFLIIP/ZhiFang.WebLis.PlatForm/ServerWCF/StatisticsService.svc/UpLoadDataCheckLogPROByEmpID';
    //7天内报告数量服务地址
    var GETREPORTCOUNTURL = uxutil.path.LOCAL + '/ZFLIIP/ZhiFang.WebLis.PlatForm/ServerWCF/StatisticsService.svc/GetAllLabReportCount';
    //当日报告列表
    var GETREPORTFORMFULLNURL = uxutil.path.LOCAL + '/ZFLIIP/ZhiFang.WebLis.PlatForm/ServerWCF/StatisticsService.svc/GetDayReportInfo';
    
    //当日报告数
    function onGetDayReportCount() {
        var Today = uxutil.date.toString(new Date(), false),
            url = GETDAYREPORTCOUNTURL + "?EmpID=" + EMPID + "&StartDateTime=" + Today + "&EndDateTime=" + Today;
        uxutil.server.ajax({ url }, function (res) {
            if (res.success) {
                $("#TodayReportCount").html(res.value);
            } else {
                $("#TodayReportCount").html(0);
            }
        });
    };
    //标本拒签数
    function onGetDCSampleRejectInfoCount() {
        var Today = new Date(),
            Year = Today.getFullYear(),
            Month = Number(Today.getMonth()) + 1,
            MonthStart = uxutil.date.getMonthFirstDate(Year, Month, true),
            MonthEnd = uxutil.date.getMonthLastDate(Year, Month, true),
            url = GETDCSAMPLEREJECTINFOCOUNTURL + "?EmpID=" + EMPID + "&StartDateTime=" + MonthStart + "&EndDateTime=" + MonthEnd;
        uxutil.server.ajax({ url }, function (res) {
            if (res.success) {
                if (res.value && res.value.length > 0) {
                    $("#SampleRejectCount").html(res.value[0]["count"]);
                } else {
                    $("#SampleRejectCount").html(0);
                }
            } else {
                $("#SampleRejectCount").html(0);
            }
        });
    };
    //当月危急值
    function onGetMonthCVCount() {
        var Today = new Date(),
            Year = Today.getFullYear(),
            Month = Number(Today.getMonth()) + 1,
            MonthStart = uxutil.date.getMonthFirstDate(Year, Month, true),
            MonthEnd = uxutil.date.getMonthLastDate(Year, Month, true),
            url = GETMONTHCVCOUNTURL + "?EmpID=" + EMPID + "&StartDateTime=" + MonthStart + "&EndDateTime=" + MonthEnd;
        uxutil.server.ajax({ url }, function (res) {
            if (res.success) {
                if (res.value && res.value.length > 0) {
                    $("#MonthCV").html(res.value[0]["count"]);
                } else {
                    $("#MonthCV").html(0);
                }
            } else {
                $("#MonthCV").html(0);
            }
        });
    };
    //当月报告回传
    function onGetMonthReportFormReBackCount() {
        var Today = new Date(),
            Year = Today.getFullYear(),
            Month = Number(Today.getMonth()) + 1,
            MonthStart = uxutil.date.getMonthFirstDate(Year, Month, true),
            MonthEnd = uxutil.date.getMonthLastDate(Year, Month, true),
            url = GETMONTHREPORTFORMREBACKCOUNTURL + "?EmpID=" + EMPID + "&StartDateTime=" + MonthStart + "&EndDateTime=" + MonthEnd;
        uxutil.server.ajax({ url }, function (res) {
            if (res.success) {
                if (res.value && res.value.length > 0) {
                    $("#MonthReportReBackCount").html(res.value[0]["count"]);
                } else {
                    $("#MonthReportReBackCount").html(0);
                }
            } else {
                $("#MonthReportReBackCount").html(0);
            }
        });
    };

    //当月报告项目TOP10
    function GetReportItemTOP10Count() {
        var Today = new Date(),
            Year = Today.getFullYear(),
            Month = Number(Today.getMonth()) + 1,
            MonthStart = uxutil.date.getMonthFirstDate(Year, Month, true),
            MonthEnd = uxutil.date.getMonthLastDate(Year, Month, true),
            url = GETREPORTITEMTOP10COUNTURL + "?EmpID=" + EMPID + "&StartDateTime=" + MonthStart + "&EndDateTime=" + MonthEnd;
        uxutil.server.ajax({ url }, function (res) {
            var seriesData = [], xData = [];
            if (res.success) {
                if (res.ResultDataValue) {
                    $.each(res.value, function (i, item) {
                        seriesData.push(item["count"]);
                        xData.push(item["TESTITEMNAME"]);
                    })
                }
            }
            setTimeout(function () {
                // 基于准备好的dom，初始化echarts实例
                var myChart = echarts.init(document.getElementById("LineChart"));
                var option = {
                    tooltip: {
                        trigger: 'axis'
                    },
                    legend: {
                        data: ['Email', 'Union Ads', 'Video Ads', 'Direct', 'Search Engine']
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            dataZoom: {
                                yAxisIndex: 'none'
                            },
                            dataView: { readOnly: false },
                            magicType: { type: ['line', 'bar'] },
                            restore: {},
                            saveAsImage: {}
                        }
                    },
                    xAxis: {
                        type: 'category',
                        boundaryGap: false,
                        axisLabel: {//坐标轴刻度标签的相关设置。
                            interval: 0,
                            rotate:45
                        },
                        data: xData
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: [{
                        name: '',
                        type: 'line',
                        data: seriesData
                    }]
                };

                myChart.setOption(option);// 使用刚指定的配置项和数据显示图表。
            });
            }, 100);
    };
    //当月报告项目量占比TOP10
    function GetReportItemTOP10CountProportion() {
        var Today = new Date(),
            Year = Today.getFullYear(),
            Month = Number(Today.getMonth()) + 1,
            MonthStart = uxutil.date.getMonthFirstDate(Year, Month, true),
            MonthEnd = uxutil.date.getMonthLastDate(Year, Month, true),
            url = GETREPORTITEMTOP10COUNTPROPORTIONURL + "?EmpID=" + EMPID + "&StartDateTime=" + MonthStart + "&EndDateTime=" + MonthEnd;
        uxutil.server.ajax({ url }, function (res) {
            var seriesData = [];
            if (res.success) {
                if (res.ResultDataValue) {
                    $.each(res.value, function (i, item) {
                        seriesData.push({ value: item["count"], name: item["TESTITEMNAME"]});
                    })
                }
            }
            // 基于准备好的dom，初始化echarts实例
            setTimeout(function () {
                var myChart = echarts.init(document.getElementById("PieChart"));
                option = {
                    tooltip: {
                        trigger: 'item'
                    },
                    legend: {
                        orient: 'vertical',
                        top: '5%',
                        left: 'left'
                    },
                    series: [
                        {
                            //name: 'Access From',
                            type: 'pie',
                            radius: ['40%', '70%'],
                            avoidLabelOverlap: false,
                            itemStyle: {
                                borderRadius: 10,
                                borderColor: '#fff',
                                borderWidth: 2
                            },
                            label: {
                                show: false,
                                position: 'center'
                            },
                            emphasis: {
                                label: {
                                    show: true,
                                    fontSize: '20',
                                    fontWeight: 'bold'
                                }
                            },
                            labelLine: {
                                show: false
                            },
                            data: seriesData
                        }
                    ]
                };
                myChart.setOption(option);// 使用刚指定的配置项和数据显示图表。
            }, 100);
        });
    };

    //当月结果回报指标分析列表
    function InitResultReturnIndexAnalysisTable() {
        var Today = new Date(),
            Year = Today.getFullYear(),
            Month = Number(Today.getMonth()) + 1,
            MonthStart = uxutil.date.getMonthFirstDate(Year, Month, true),
            MonthEnd = uxutil.date.getMonthLastDate(Year, Month, true),
            url = GETRESULTRETURNANINDEXALYSISURL + "?EmpID='" + EMPID + "'&StartDateTime=" + MonthStart + "&EndDateTime=" + MonthEnd + "&sort=[{ 'property': 'DataAddTime', 'direction': 'desc' }]";
        
        table.render({
            elem: "#ResultReturnIndexAnalysisTable",
            id: "ResultReturnIndexAnalysisTable",
            url: url,
            height: 'full-400',
            data: [],
            autoSort: false,
            size: 'sm',
            page: false, //分页
            limit: 1000, //每页显示20
            limits: [10, 20, 40, 50], //显示数量选择
            toolbar: false,
            loading: false,
            cols: [[
                { field: 'WebLisSourceOrgName', minWidth: 120, title: '机构名称', sort: false },
                { field: 'uniformity', width: 80, title: '一致性数量', sort: false },
                { field: 'Integrity', width: 80, title: '完整性数量', sort: false },
                { field: 'timeliness', width: 80, title: '及时性数量', sort: false }
            ]
            ],
            text: { none: '暂无相关数据' },
            response: function () {
                return {
                    statusCode: true, //成功状态码
                    statusName: 'code', //code key
                    msgName: 'msg ', //msg key
                    dataName: 'data' //data key
                }
            },
            parseData: function (res) {//res即为原始返回的数据
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.length || 0, //解析数据长度
                    "data": data || []
                };
            },
            done: function () { }
        });
    };
    //7天内报告数量
    function InitInfectionItemTable() {
        var Today = new Date(),
            EndDateTime = uxutil.date.toString(Today, true),
            StartDateTime = uxutil.date.toString(uxutil.date.getNextDate(EndDateTime, -7), true),
            url = GETREPORTCOUNTURL + "?EmpID='" + EMPID + "'&StartDateTime=" + StartDateTime + "&EndDateTime=" + EndDateTime;

        table.render({
            elem: "#InfectionItemTable",
            id: "InfectionItemTable",
            url: url,
            height: 'full-400',
            data: [],
            autoSort: false,
            size: 'sm',
            page: false, //分页
            limit: 1000, //每页显示20
            limits: [10, 20, 40, 50], //显示数量选择
            toolbar: false,
            loading: false,
            cols: [[
                { field: 'LabName', minWidth: 120, title: '机构名称', sort: false },
                { field: 'Count', width: 80, title: '报告数量', sort: false }
            ]
            ],
            text: { none: '暂无相关数据' },
            response: function () {
                return {
                    statusCode: true, //成功状态码
                    statusName: 'code', //code key
                    msgName: 'msg ', //msg key
                    dataName: 'data' //data key
                }
            },
            parseData: function (res) {//res即为原始返回的数据
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.length || 0, //解析数据长度
                    "data": data || []
                };
            },
            done: function () { }
        });
    };
    //当日报告列表
    function InitReportFormFullTable() {
        var Today = uxutil.date.toString(new Date(), true),
            NextDay = uxutil.date.toString(uxutil.date.getNextDate(Today, 1), true),
            url = GETREPORTFORMFULLNURL + "?isPlanish=true&where=RECEIVEDATE>='" + Today + "' and RECEIVEDATE<'" + NextDay + "'&sort=[{'property':'ReportFormFull_RECEIVEDATE','direction':'desc'}]";
        table.render({
            elem: "#ReportFormFullTable",
            id: "ReportFormFullTable",
            url: url,
            height: 'full-400',
            data: [],
            autoSort: false,
            size: 'sm',
            page: false, //分页
            limit: 1000, //每页显示20
            limits: [10, 20, 40, 50], //显示数量选择
            toolbar: false,
            loading: false,
            cols: [[
                {
                    field: 'ReportFormFull_WebLisOrgName',
                    title: '机构名称', sort: false,
                    minWidth: 100,
                },
                {
                    field: 'ReportFormFull_CNAME',
                    title: '姓名', sort: false,
                    width: 70
                },
                {
                    field: 'ReportFormFull_DEPTNAME',
                    title: '科室', sort: false,
                    width: 80
                },
                {
                    field: 'ReportFormFull_SICKTYPENAME',
                    title: '就诊类型', sort: false,
                    width: 70
                },
                {
                    field: 'ReportFormFull_SAMPLETYPENAME',
                    title: '样本类型', sort: false,
                    width: 70
                }, {
                    field: 'ReportFormFull_PDFFILE',
                    title: '路径', sort: false,
                    hide: true,
                    width: 180
                },
                {
                    field: 'ReportFormFull_SECTIONNO',
                    title: '小组ID',
                    hide: true
                },
                {
                    field: 'ReportFormFull_Id',
                    title: '主键ID',
                    hide: true
                }
                ]
            ],
            text: { none: '暂无相关数据' },
            response: function () {
                return {
                    statusCode: true, //成功状态码
                    statusName: 'code', //code key
                    msgName: 'msg ', //msg key
                    dataName: 'data' //data key
                }
            },
            parseData: function (res) {//res即为原始返回的数据
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function () { }
        });
    };

    //初始化
    function init() {
        onGetDayReportCount();
        onGetDCSampleRejectInfoCount();
        onGetMonthCVCount();
        onGetMonthReportFormReBackCount();
        GetReportItemTOP10Count();
        GetReportItemTOP10CountProportion();
        InitResultReturnIndexAnalysisTable();
        InitInfectionItemTable();
        InitReportFormFullTable();
    };
    //初始化
    init();
});