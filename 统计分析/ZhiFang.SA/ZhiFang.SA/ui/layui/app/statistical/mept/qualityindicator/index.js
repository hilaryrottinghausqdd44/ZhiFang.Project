
layui.extend({
    uxutil: 'ux/util',
    echarts: 'src/echarts/echarts',
    //echarts: 'admin/layuiadmin/lib/extend/echarts'
    dateutil: 'app/statistical/modules/dateutil',
    qiTypeEChart: 'app/statistical/mept/qualityindicator/qiTypeEChart'
}).use(["uxutil", 'element', "carousel", "echarts", "dateutil", "qiTypeEChart"], function () {
    "use strict";

    var $ = layui.$;
    var uxutil = layui.uxutil;
    var element = layui.element;
    var carousel = layui.carousel;
    var echarts = layui.echarts;// (layui.carousel, layui.echarts);
    var qiTypeEChart = layui.qiTypeEChart;
    var dateutil = layui.dateutil;

    //carousel初始化
    $(".layadmin-carousel").each(function () {
        var elem = $(this);
        carousel.render({
            elem: this,
            width: "100%",
            height: "100%",
            arrow: "none",
            interval: elem.data("interval"),
            autoplay: elem.data("autoplay") === !0,
            //trigger: t.ios || t.android ? "click" : "hover",
            anim: elem.data("anim")
        })
    }), element.render("progress");

    //先加载质量指标类型分类 

    //接收传入参数
    var params = uxutil.params.get();
    var qitype = "",
        pwclassid = "";
    //指标所属分类的类型ID
    if (params["qitype"]) qitype = params["qitype"];
    //指标类型ID
    if (params["id"]) pwclassid = params["id"];

    //初始化第一行第一列
    function initRow11() {
        var url = uxutil.path.ROOT + "/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchFailedTotalAndSpecimenTotalOfYearAndMonth";
        var arrHql = [];
        var date = new Date;
        var year = date.getFullYear();//本年
        var month = "" + year + "-" + date.getMonth();//上月
        //统计结果分类:LStatTotalClassification(质量指标类型)
        arrHql.push('classificationId=1');
        if (qitype) {
            arrHql.push('qitype=' + qitype);
        }
        if (pwclassid) {
            arrHql.push('pwclassid=' + pwclassid);
        }
        if (year) {
            arrHql.push('year=' + year);
        }
        if (month) {
            arrHql.push('month=' + month);
        }
        if (arrHql && arrHql.length > 0) {
            arrHql = arrHql.join("&");
        } else {
            arrHql = "";
        }
        url += (url.indexOf('?') == -1 ? '?' : '&') + arrHql;
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data.success) {
                setRow11(data.value);
            } else {

            }
        });
    };
    //第一行第一列赋值
    function setRow11(data) {
        //质量类型标题处理
        if (data.Title) {
            $("#LAY-echarts-row1-col1-title").html(data.Title);
        }
        //上月统计赋值处理
        if (data.Month) {
            if (!data.Month.FailedTotal) data.Month.FailedTotal = 0;
            var failedTotal = "E:&nbsp;" + data.Month.FailedTotal;
            $("#LAY-echarts-row1-col1-lastMonth-failedTotal").html(failedTotal);

            if (!data.Month.SpecimenTotal) data.Month.SpecimenTotal = 0;
            var specimenTotal = "T:&nbsp;" + data.Month.SpecimenTotal;
            $("#LAY-echarts-row1-col1-lastMonth-specimenTotal").html(specimenTotal);
        }
        //本年统计赋值
        if (data.Year) {
            if (!data.Year.FailedTotal) data.Year.FailedTotal = 0;
            var failedTotal = "E:&nbsp;" + data.Year.FailedTotal;
            $("#LAY-echarts-row1-col1-curYear-failedTotal").html(failedTotal);

            if (!data.Year.SpecimenTotal) data.Year.SpecimenTotal = 0;
            var specimenTotal = "T:&nbsp;" + data.Year.SpecimenTotal;
            $("#LAY-echarts-row1-col1-curYear-specimenTotal").html(specimenTotal);
        }
    };
    //初始化第一行第二列(按月/按季度/按年)
    function initRow12() {
        //第一行第二列
        var options12 = {
            /**图表所在的div*/
            elem: "LAY-echarts-row1-col2",
            /**统计结果分类:LStatTotalClassificatione的Id值;质量指标类型值为1*/
            classificationId: "1",
            /**指标类型所属分类:QualityIndicatorType的Id值*/
            qitype: qitype,
            /**指标类型字典(PhrasesWatchClass)Id值*/
            pwclassid: pwclassid,
            /**统计日期类型:LStatTotalStatDateType的Id值*/
            statDateType: "2",//默认按月
            /**统计维度:各质量指标分类类型对应的统计维度的Id值*/
            sadimension: "1",//统计纬度第一项
        };
        var meEChart12 = qiTypeEChart.render(options12);
        return meEChart12;
    };
    //初始化第二行第一列(上月)
    function initRow21() {
        var startDate = dateutil.getLastMonthStartDate();
        var endDate = dateutil.getLastMonthEndDate();
        var options21 = {
            /**图表所在的div*/
            elem: "LAY-echarts-row2-col1",
            /**统计结果分类:LStatTotalClassificatione的Id值;质量指标类型值为1*/
            classificationId: "1",
            /**指标类型所属分类:QualityIndicatorType的Id值*/
            qitype: qitype,
            /**指标类型字典(PhrasesWatchClass)Id值*/
            pwclassid: pwclassid,
            /**统计日期类型:LStatTotalStatDateType的Id值*/
            statDateType: "2",
            /**统计维度:各质量指标分类类型对应的统计维度的Id值*/
            sadimension: "1",//统计纬度第一项
            /**开始日期*/
            startDate: startDate,
            /**结束日期 */
            endDate: endDate
        };
        var meEChart21 = qiTypeEChart.render(options21);
        return meEChart21;
    };
    //初始化第二行第二列(由第一行第二列图表联动):某月/季度/某年
    function initRow22(year, month) {
        var startDate = dateutil.getMonthStartDate(year, month);
        var endDate = dateutil.getMonthEndDate(year, month);
        var options22 = {
            /**图表所在的div*/
            elem: "LAY-echarts-row2-col2",
            /**统计结果分类:LStatTotalClassificatione的Id值;质量指标类型值为1*/
            classificationId: "1",
            /**指标类型所属分类:QualityIndicatorType的Id值*/
            qitype: qitype,
            /**指标类型字典(PhrasesWatchClass)Id值*/
            pwclassid: pwclassid,
            /**统计日期类型:LStatTotalStatDateType的Id值*/
            statDateType: "2",//月份/季度/年份
            /**统计维度:各质量指标分类类型对应的统计维度的Id值*/
            sadimension: "1",//统计纬度第一项
            /**开始日期*/
            startDate: startDate,
            /**结束日期 */
            endDate: endDate
        };
        var meEChart22 = qiTypeEChart.render(options22);
        return meEChart22;
    };
    //初始化第三行第一列(本年)
    function initRow31(year, month) {
        var date = new Date();
        var year = date.getFullYear();//本年
        var month = date.getMonth();//上月       
        var startDate = dateutil.getMonthStartDate(year, 0);//一月
        var endDate = dateutil.getMonthEndDate(year, month);//上月  
        var options31 = {
            /**图表所在的div*/
            elem: "LAY-echarts-row3-col1",
            /**统计结果分类:LStatTotalClassificatione的Id值;质量指标类型值为1*/
            classificationId: "1",
            /**指标类型所属分类:QualityIndicatorType的Id值*/
            qitype: qitype,
            /**指标类型字典(PhrasesWatchClass)Id值*/
            pwclassid: pwclassid,
            /**统计日期类型:LStatTotalStatDateType的Id值*/
            statDateType: "6",//本年
            /**统计维度:各质量指标分类类型对应的统计维度的Id值*/
            sadimension: "4",//统计纬度第一项
            /**开始日期*/
            startDate: startDate,
            /**结束日期 */
            endDate: endDate
        };
        var meEChart31 = qiTypeEChart.render(options31);
        return meEChart31;
    };
    //初始化第三行第二列(由第一行第二列图表联动)
    function initRow22(year, month) {
        var startDate = dateutil.getMonthStartDate(year, month);
        var endDate = dateutil.getMonthEndDate(year, month);
        var options32 = {
            /**图表所在的div*/
            elem: "LAY-echarts-row3-col2",
            /**统计结果分类:LStatTotalClassificatione的Id值;质量指标类型值为1*/
            classificationId: "1",
            /**指标类型所属分类:QualityIndicatorType的Id值*/
            qitype: qitype,
            /**指标类型字典(PhrasesWatchClass)Id值*/
            pwclassid: pwclassid,
            /**统计日期类型:LStatTotalStatDateType的Id值*/
            statDateType: "2",//月份/季度/年份
            /**统计维度:各质量指标分类类型对应的统计维度的Id值*/
            sadimension: "1",//统计纬度第一项
            /**开始日期*/
            startDate: startDate,
            /**结束日期 */
            endDate: endDate
        };
        var meEChart32 = qiTypeEChart.render(options32);
        return options32;
    };
    //初始化
    function initAll() {
        //第一行第一列
        initRow11();
        //第一行第二列
        var meEChart12 =initRow12();
        //第二行第一列
        var meEChart21 =initRow21();

        //第三行第一列
        var meEChart31 = initRow31();

        //第二行第二列(由第一行第二列图表联动)

        //第三行第二列(由第一行第二列图表联动)

    };
    initAll();

});
