/**
   @Name：评语
   @Author：zhangda
   @version 2019-11-26
*/
//外部调用
var CALLBACK = null;
var externalCallFun = function (callback) { CALLBACK = callback; };
layui.extend({
    uxutil: 'ux/util',
    CommentPhraseTable:'views/sample/basic/phrase/CommentPhraseTable'
}).use(['uxutil', 'table', 'form','CommentPhraseTable'], function () {
    var $ = layui.$,
        form = layui.form,
        uxutil = layui.uxutil,
        CommentPhraseTable = layui.CommentPhraseTable,
        table = layui.table;
    //全局变量
    var config = {
        getPinYinZiTouUrl: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPinYinZiTou?chinese=',//获得拼音字头
    };

    var paramsObj = {//isAppendValue：文本域是追加值还是替换值  //isReadonly：文本域是否只读
        ObjectType: null,//针对类型1：小组样本 2：检验项目
        ObjectID: null,//小组或仪器等ID
        PhraseType: null,//短语类型 --枚举短语类别 例如，SamplePhrase， ItemPhrase
        TypeCode: null,//短语类型Code --枚举
        TypeName: null,//短语类型 例如：项目结果短语  --枚举
        SampleTypeID:null,//样本类型
        CName: null, isReadonly: false, isAppendValue: true
    };
    //初始化
    init();
    //初始化
    function init() {
        //显示遮罩层
        var indexs = layer.load();
        getParams();//获得参数
        initCommentPhraseTable();
        initGroupListeners();//监听联动
        if (paramsObj.isReadonly) {
            $("#Comment").prop("readonly","readonly");
        }
        layer.closeAll('loading');
    }
    //获得参数
    function getParams() {
        if (location.search.indexOf("?") == -1) return;
        var params = location.search.split("?")[1].split("&");
        //参数赋值
        for (var j in paramsObj) {
            for (var i = 0; i < params.length; i++) {
                if (j.toUpperCase() == params[i].split("=")[0].toUpperCase()) {
                    paramsObj[j] = (decodeURIComponent(params[i].split("=")[1]) == 0 || decodeURIComponent(params[i].split("=")[1]) == false || decodeURIComponent(params[i].split("=")[1]) == "false") ? false : decodeURIComponent(params[i].split("=")[1]);
                }
            }
        }
    }
    //联动
    function initGroupListeners() {
        $("#LBPhraseCNameLabel").html(paramsObj.CName);//赋值短语名称label
        //短语名称填写完赋值拼音字头和快捷码
        $("#LBPhrase_CName").blur(function () {
            getPinYinZiTou($(this).val(), function (str) {
                $("#LBPhrase_PinYinZiTou").val(str);
                $("#LBPhrase_Shortcode").val(str);
            });
        });
        //关闭按钮
        $("#close").click(function () {
            var value = $("#Comment").val();
            closeBtnClick(value);
        });
        //行双击
        layui.onevent("close", "close", function (obj) {
            closeBtnClick(obj.value);
        })
    }
    //评语短语渲染’
    function initCommentPhraseTable() {
        //评语短语列表功能参数配置
        var Obj = {
            elem: '#CommentPhraseTable',
            title: '评语短语渲染',
            height: 'full-400',
            id: 'CommentPhraseTable',
            defaultParams: {}
        };
        CommentPhraseTable.render(Obj, paramsObj.CName, paramsObj.ObjectType, paramsObj.ObjectID, paramsObj.PhraseType, paramsObj.TypeCode, paramsObj.TypeName, paramsObj.SampleTypeID, paramsObj.isAppendValue);
    }
    //拼音字头
    function getPinYinZiTou(val, callback) {
        var me = this;
        var url = config.getPinYinZiTouUrl + encodeURI(val);
        if (val == "") {
            if (typeof (callback) == "function") {
                callback(val);
            }
            return;
        }
        $.ajax({
            type: "get",
            url: url,
            dataType: 'json',
            success: function (res) {
                if (res.success) {
                    if (typeof (callback) == "function") {
                        callback(res.ResultDataValue);
                    }
                } else {
                    layer.msg("拼音字头获得失败！", { icon: 5, anim: 6 });
                }
            }
        });
    };
    //关闭事件
    function closeBtnClick(value) {
        CALLBACK(value);
        var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
        parent.layer.close(index); //再执行关闭
    }
});