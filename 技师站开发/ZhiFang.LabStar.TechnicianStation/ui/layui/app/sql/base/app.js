/**
   @Name：数据库基础执行脚本
   @Author：zhangda
   @version 2019-10-25
 */
layui.extend({
    uxutil: 'ux/util',
    QuerySQLTable: 'app/sql/base/QuerySQLTable',
}).use(['uxutil', 'table', 'form', 'element','QuerySQLTable'], function () {
    var $ = layui.$,
        element = layui.element,
        form = layui.form,
        uxutil = layui.uxutil,
        table = layui.table,
        QuerySQLTable = layui.QuerySQLTable;
    //全局变量
    var config = {
        //当前激活页签,默认第一个,从0开始
        currTabIndex: 0,
        //执行语句服务地址
        ExecSQLUrl: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/ExecSQL',
        //查询语句服务地址
        QuerySQLUrl: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/QuerySQL'
    };
    //初始化
    init();
    //初始化
    function init() {
        //设置文本域高度
        $("#ExecSQL").css("height", ($(window).height() - 175) + "px");
        $("#QuerySQL").css("height", "125px");
        //查询结果表格渲染
        initQuerySQLTable();
        //监听联动
        initGroupListeners();
    }
    //获得选中内容
    function getValue() {
        if (window.getSelection) {//一般浏览器
            userSelection = window.getSelection();
        } else if (document.selection) {
            //IE浏览器、Opera
            userSelection = document.selection.createRange();
        }
        var str = "";
        try {
            str += "" + userSelection.toString();
        } catch (e) {//I兼容IE
            str += "" + userSelection.text;
        }
        return str;
    }
    //提交sql 执行
    function ExecuteSQL() {
        var sql = "";
        var url = "";
        var formId = "ExecSQLForm";
        switch (config.currTabIndex) {
            case 0:
                sql = $("#ExecSQL").val();
                url = config.ExecSQLUrl;
                formId = "ExecSQLForm";
                break;
            case 1:
                sql = $("#QuerySQL").val();
                url = config.QuerySQLUrl;
                formId = "QuerySQLForm";
                break;
            default:
                break;
        }
        if (sql == "") {
            layer.msg("请输入SQL脚本!");
            return;
        }
        var selectStr = getValue();
        if (sql.indexOf(selectStr) != -1) {//选中内容是文本域中的内容
            sql = selectStr == "" ? sql : selectStr;
        }
        layer.confirm('确定执行该SQL脚本?', { icon: 3, title: '提示' }, function (index) {
            //显示遮罩层
            var indexs = layer.load();
            $("#" + formId).children("input[name=strSQL]").val(sql);//赋值到隐藏框中
            $("#" + formId).attr("action", url);//form的action赋值
            document.getElementById(formId).submit();//执行提交
            //获得submit提交返回数据
            $("#targetIfr").load(function () {
                var text = $(this).contents().find("body").text();//获取到的是json的字符串
                var res = $.parseJSON(text);//json字符串转换成json对象
                layer.closeAll();//关闭遮罩和确认框
                if (res.success) {
                    switch (config.currTabIndex) {
                        case 0:
                            layer.alert(res.ResultDataValue + '行受影响', {
                                closeBtn: 0,
                                title: '执行结果'
                            }, function (index) {
                                layer.close(index);
                            });
                            break;
                        case 1:
                            if (res.ResultDataValue != "" && res.ResultDataValue != null) {
                                var data = JSON.parse(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n"));
                                QuerySQLTable.onSearch('QuerySQLTable', data);
                            } else {
                                layer.msg("没有查询到任何数据,请检查查询语句!");
                            }
                            break;
                        default:
                            break;
                    }
                } else {
                    layer.msg(res.ErrorInfo, { icon: 5, anim: 6 });
                    if (config.currTabIndex == 1) QuerySQLTable.onSearch('QuerySQLTable', []);
                }
            });
        });
    }
    //联动
    function initGroupListeners() {
        //监听执行按钮点击事件
        $("#execute").click(function () {
            ExecuteSQL();
        });
        //监听F5按下事件
        $(document).keydown(function (event) {
            switch (event.keyCode) {
                case 116:
                    event.preventDefault();
                    ExecuteSQL();
            }
        });
        //监听点击事件 选中内容继续选中
        $(document).click(function (e) {
            if (e.target.tagName != "BUTTON" && e.target.tagName != "A") return;
            switch (config.currTabIndex) {
                case 0:
                    $("#ExecSQL").focus();
                    break;
                case 1:
                    $("#QuerySQL").focus();
                    break;
                default:
                    break;
            }
        });
    }
    //页签切换
    element.on('tab(tab)', function (obj) {
        config.currTabIndex = obj.index;
    });
    //查询结果渲染’
    function initQuerySQLTable() {
        var Obj = {
            elem: '#QuerySQLTable',
            title: '查询结果',
            height: 'full-350',
            id: 'QuerySQLTable'
        };
        QuerySQLTable.render(Obj);
    }
});