/**
   @Name：短语
   @Author：zhangda
   @version 2021-10-16
*/
//外部调用
var CALLBACK = null;
var externalCallFun = function (callback) { CALLBACK = callback; };
layui.extend({
    uxutil: 'ux/util',
    uxbase: 'ux/base'
}).use(['uxutil','uxbase', 'table', 'form','element'], function () {
    var $ = layui.$,
        uxutil = layui.uxutil,
        uxbase = layui.uxbase,
        form = layui.form,
        element = layui.element,
        table = layui.table;

    //获得拼音字头服务地址
    var GETPINYINZITOUURL = uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPinYinZiTou?chinese=';
    //获得样本类型服务地址
    var GETSAMPLETTPEURL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true';
    //获得短语服务地址
    var SELECTURL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?isPlanish=true';
    //新增短语服务地址
    var ADDURL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrase';
    //编辑短语服务地址
    var UPDATEURL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhraseByField';
    //删除短语服务地址
    var DELURL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrase';

    //外部参数
    var PARAMS = uxutil.params.get(true);
    //针对类型 --1：小组样本 2：检验项目
    var OBJECTTYPE = PARAMS.OBJECTTYPE || null;
    //针对类型ID 小组ID，或者项目ID等
    var OBJECTID = PARAMS.OBJECTID || null;
    //短语类型 --枚举短语类别 例如，SamplePhrase， ItemPhrase
    var PHRASETYPE = PARAMS.PHRASETYPE || null;
    //短语类型Code 例如：XMJGDY --枚举
    var TYPECODE = PARAMS.TYPECODE || null;
    //短语类型名称 例如：项目结果短语  --枚举
    var TYPENAME = PARAMS.TYPENAME || null;
    //样本类型
    var SAMPLETYPEID = PARAMS.SAMPLETYPEID || null;
    //使用启用样本类型
    //var ISENABLESAMPLETYPEID = !PARAMS.ISENABLESAMPLETYPEID || String(PARAMS.ISENABLESAMPLETYPEID) == 'false' || String(PARAMS.ISENABLESAMPLETYPEID) == '0' ? 0 : 1;
    //当前短语维护Lable
    var CNAME = PARAMS.CNAME || null;
    //是否只读  1：是 0：false
    var ISREADONLY = PARAMS.ISREADONLY && String(PARAMS.ISREADONLY) != 'false' && String(PARAMS.ISREADONLY) != '0' ? 1 : 0;
    //是否追加值  1：追加 0：替换
    var ISAPPENDVALUE = PARAMS.ISAPPENDVALUE && String(PARAMS.ISAPPENDVALUE) != 'false' && String(PARAMS.ISAPPENDVALUE) != '0' ? 1 : 0;
    //是否换行加入值 1：加\n 0：''
    var ISNEXTLINEADD = PARAMS.ISNEXTLINEADD && String(PARAMS.ISNEXTLINEADD) != 'false' && String(PARAMS.ISNEXTLINEADD) != '0' ? 1 : 0;

    //短语维护列表实例
    var PHRASETABLEINSTANCE = null;
    //样本类型Map
    var SAMPLETYPEMAP = {};
    //列表ID
    var TABLEID = 'table';
    //列表选中数据
    var CHECKROWDATA = [];
    //表单状态
    var FORMTYPE = 'add';
    //最大显示次序
    var MAXDISPORDER = 1;

    //获取所有样本类型信息
    function InitSampleType(callback) {
        //暂时只有项目短语维护样本类型，样本短语暂时先不维护样本类型 查询时也不用判断样本类型
        if (OBJECTTYPE != 2) {
            callback && callback();
            return;
        }
        var html = ['<option value="">请选择</option>'],
            url = GETSAMPLETTPEURL;
        url += '&sort=[{property:"LBSampleType_DispOrder",direction:"ASC"}]';
        url += '&where=IsUse=1';
        url += '&fields=LBSampleType_Id,LBSampleType_CName';
        SAMPLETYPEMAP = {};
        var load = layer.load();
        uxutil.server.ajax({ url: url }, function (res) {
            layer.close(load);
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    if (data.length > 0) {
                        for (var i in data) {
                            html.push('<option value="' + data[i].LBSampleType_Id+'">' + data[i].LBSampleType_CName+'</option>');
                            SAMPLETYPEMAP[data[i].LBSampleType_Id] = data[i].LBSampleType_CName;
                        }
                        $("#LBPhrase_SampleTypeID").html(html.join(''));
                        form.render();
                    }
                }
            } else {
                uxbase.MSG.onError("样本类型字典查询失败!" + res.ErrorInfo);
            }
            callback && callback();
        });
    };
    //初始化列表
    function InitTable(callback) {
        var url = GetTableSelectUrl() || SELECTURL + '&sort=[{property:"LBPhrase_DispOrder",direction:"ASC"}]';

        PHRASETABLEINSTANCE = table.render({
            elem: '#' + TABLEID,
            height: 'full-185',
            url: url,
            toolbar: '',
            page: false,
            limit: 1000,
            //limits: [100, 200, 500, 1000, 1500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号', fixed: 'left' },
                { field: 'LBPhrase_Id', width: 150, title: '主键', sort: false, hide: true },
                { field: 'LBPhrase_CName', minWidth: 200, title: CNAME, sort: true },
                {
                    field: 'LBPhrase_SampleTypeID', width: 140, title: '样本类型', sort: false,
                    templet: function (data) {
                        return SAMPLETYPEMAP[data.LBPhrase_SampleTypeID] || "";
                    }
                },
                { field: 'LBPhrase_PinYinZiTou', width: 100, title: '拼音字头', sort: false },
                { field: 'LBPhrase_DispOrder', width: 80, title: '显示次序', sort: false }
            ]],
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                //加载完毕处理
                callback && callback(res.data);
                //无数据处理
                if (count == 0) {
                    FORMTYPE = 'add';
                    onClear();
                    MAXDISPORDER = 1;
                    return;
                }
                MAXDISPORDER = Number(res.data[res.data.length - 1]["LBPhrase_DispOrder"]) +1;
                //触发点击事件
                $("#" + TABLEID + "+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();

            }
        });
    };
    //刷新列表处理
    function onSearch() {
        var url = GetTableSelectUrl() || SELECTURL + '&sort=[{property:"LBPhrase_DispOrder",direction:"ASC"}]';
        PHRASETABLEINSTANCE.reload({
            url: url,
            where: {
                time: new Date().getTime()
            }
        });
    };
    //获得查询短语地址
    function GetTableSelectUrl() {
        var url = SELECTURL + '&sort=[{property:"LBPhrase_DispOrder",direction:"ASC"}]',
            where = ['IsUse=1'];
        //针对类型
        if (OBJECTTYPE) where.push("ObjectType='" + OBJECTTYPE + "'");
        //针对类型ID
        if (OBJECTID) where.push("ObjectID='" + OBJECTID + "'");
        //样本类型
        if (String(SAMPLETYPEID) != 'null')
            where.push("(SampleTypeID is null or SampleTypeID='" + SAMPLETYPEID + "')");
        else
            where.push("SampleTypeID is null");
        //短语类型
        where.push("PhraseType='" + PHRASETYPE + "'");
        //类型代码
        where.push("TypeCode='" + TYPECODE + "'");

        url += '&where=' + where.join(' and ');

        return url;
    };
    //根据列表同步复选内容
    function GetCheckBoxByTable(list) {
        var list = list || table.cache[TABLEID] || [],
            Comment = $("#Comment").val(),
            html = [];
        $.each(list, function (i, item) {
            var checked = Comment && Comment.indexOf(item["LBPhrase_CName"]) != -1 ? true : false;//存在则选中
            html.push('<div class="layui-form-item"><input type="checkbox" name="phrase" lay-filter="phrase" title="' + item["LBPhrase_CName"] + '" lay-skin="primary"' + (checked ? " checked" : "")+'></div>');
        });
        $("#checkboxcontent").html(html.join(''));
        form.render();
    };
    //拼音字头
    function GetPinYinZiTou(val, callback) {
        var url = GETPINYINZITOUURL + encodeURI(val);
        if (val == "") {
            if (typeof (callback) == "function") callback(val);
            return;
        }
        uxutil.server.ajax({ url: url }, function (res) {
            if (res.success) {
                if (typeof (callback) == "function") callback(res.ResultDataValue);
            }
        });
    };
    //关闭事件
    function CloseBtnClick(value) {
        if (value != null && value != 'undefined') CALLBACK(value);//.replace(/\n/g, "\\n")
        var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
        parent.layer.close(index); //再执行关闭
    }
    //阻止默认事件
    function onPreventDefault() {
        var device = layui.device();
        if (device.ie) {
            window.event.returnValue = false;
        } else {
            window.event.preventDefault();
        }
    };
    //清空短语表单
    function onClear() {
        $("#AddPhraseForm :input").each(function () {
            $(this).val('');
        });
    };
    //赋值短语表单
    function onEdit() {
        if (CHECKROWDATA.length == 0) {
            onClear();
            return;
        }

        $("#LBPhrase_CName").val(CHECKROWDATA[0]["LBPhrase_CName"]);
        $("#LBPhrase_SampleTypeID").val(CHECKROWDATA[0]["LBPhrase_SampleTypeID"]);
        $("#LBPhrase_PinYinZiTou").val(CHECKROWDATA[0]["LBPhrase_PinYinZiTou"]);
        $("#LBPhrase_Shortcode").val(CHECKROWDATA[0]["LBPhrase_Shortcode"]);
        $("#LBPhrase_DispOrder").val(CHECKROWDATA[0]["LBPhrase_DispOrder"]);
        form.render();
    };
    //新增
    function onAddClick() {
        FORMTYPE = 'add';
        onSetEditStatusText();
        onClear();
        $("#LBPhrase_DispOrder").val(MAXDISPORDER);
        $("#LBPhrase_CName").focus();
    };
    //保存
    function onSaveClick() {
        var url = FORMTYPE == 'add' ? ADDURL : UPDATEURL;
        var entitys = {};
        var field = [];
        var entity = {
            PhraseType: PHRASETYPE,
            TypeCode: TYPECODE,
            TypeName: TYPENAME,
            CName: $("#LBPhrase_CName").val(),
            SampleTypeID: $("#LBPhrase_SampleTypeID").val() ? $("#LBPhrase_SampleTypeID").val() : null,
            Shortcode: $("#LBPhrase_Shortcode").val(),
            PinYinZiTou: $("#LBPhrase_PinYinZiTou").val(),
            IsUse: true,
            DispOrder: FORMTYPE == 'add' ? table.cache[TABLEID].length + 1 : CHECKROWDATA[0]["LBPhrase_DispOrder"]
        };
        if (OBJECTTYPE) entity.ObjectType = OBJECTTYPE;
        if (OBJECTID) entity.ObjectID = OBJECTID;
        //编辑状态加字段
        if (FORMTYPE == 'edit') {
            entity.Id = CHECKROWDATA[0]["LBPhrase_Id"];
            for (var i in entity) {
                field.push(i);
            }
            entitys.fields = field.join(",");
        }
        entitys.entity = entity;

        var config = {
            type: "POST",
            url: url,
            data: JSON.stringify(entitys)
        };
        var indexs = layer.load();
        uxutil.server.ajax(config, function (data) {
            layer.close(indexs);//隐藏遮罩层
            if (data.success) {
                onSearch();
            } else {
                uxbase.MSG.onError(data.msg);
            }
        });
    };
    //删除
    function onDelClick() {
        if (CHECKROWDATA.length == 0) return;
        var id = CHECKROWDATA[0].LBPhrase_Id;
        if (!id) return;
        var url = DELURL + '?id=' + id;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var loadIndex = layer.load();//显示遮罩层
            uxutil.server.ajax({
                url: url
            }, function (data) {
                layer.close(loadIndex);//隐藏遮罩层
                if (data.success === true) {
                    layer.close(index);
                    uxbase.MSG.onSuccess("删除成功!");
                    onSearch();
                } else {
                    uxbase.MSG.onError(data.msg);
                }
            });
        });
    };
    //表单编辑状态
    function onSetEditStatusText() {
        $("#FormType").html(FORMTYPE == 'add' ? "【新增】" : (FORMTYPE == 'edit' ? "【编辑】" : "【查看】"));
    };
    //过滤复选
    function onFilter() {
        var FilterStr = $("#filter").val(),
            tablecache = table.cache[TABLEID] || [],
            list = [];

        if (FilterStr == "") {
            GetCheckBoxByTable(tablecache);
            return;
        }

        $.each(tablecache, function (i, item) {
            if (item["LBPhrase_CName"].indexOf(FilterStr) != -1) list.push(item);
        });

        GetCheckBoxByTable(list);
    };
    //监听
    function InitListeners() {
        var iTIME = null;
        //短语名称动态赋值拼音字头和快捷码
        $('#LBPhrase_CName').on("input propertychange", function () {
            clearTimeout(iTIME);
            iTIME = setTimeout(function () {
                GetPinYinZiTou($('#LBPhrase_CName').val(), function (str) {
                    $("#LBPhrase_PinYinZiTou").val(str);
                    $("#LBPhrase_Shortcode").val(str);
                });
            },200);
        });
        //复选框选择处理
        form.on('checkbox(phrase)', function (data) {
            var checked = data.elem.checked,
                title = $(data.elem).attr("title"),
                Text = $("#Comment").val();//文本域值
            if (checked) {//加入
                if (!ISAPPENDVALUE) Text = "";
                if (ISNEXTLINEADD) {//是否换行加入
                    Text = Text + (Text ? '\n' : "") + title;
                } else {
                    Text += title;
                }
            } else {//删除
                if (Text.indexOf(title) != -1) {
                    Text = Text.replace(title,'');
                }
            }
            $("#Comment").val(Text);
        });
        //监听行单击事件
        table.on('row(' + TABLEID + ')', function (obj) {
            CHECKROWDATA = [];
            CHECKROWDATA.push(obj.data);
            FORMTYPE = 'edit';
            onSetEditStatusText();
            onEdit();
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        });
        //监听回车按下
        $(document).keydown(function (event) {
            switch (event.keyCode) {
                case 13://回车 -- 样本号
                    //判断焦点是否在该输入框
                    if (document.activeElement == document.getElementById("filter")) onFilter();
                    break;
            }
        });
        //新增按钮
        $("#add").on('click', function () {
            onAddClick();
        });
        //保存按钮
        form.on('submit(save)', function (data) {
            onPreventDefault();
            onSaveClick();
            return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
        });
        //删除按钮
        $("#del").on('click', function () {
            onDelClick();
        });
        //确认按钮
        $("#ok").on('click',function () {
            var value = $("#Comment").val();
            CloseBtnClick(value);
        });
        //取消按钮
        $("#cancel").on('click', function () {
            CloseBtnClick();
        });
    }
    //初始化HTML
    function InitHtml() {
        //设置复选内容高度
        var height = $(window).height() - 220 > 100 ? $(window).height() - 220 : 100;
        $("#checkboxcontent").css("height", height+"px");
        //文本域只读处理
        if (String(ISREADONLY) != 'false' && String(ISREADONLY) != '0')
            $("#Comment").prop("readonly", true);
        else
            $("#Comment").prop("readonly", false);
        //替换当前短语名称
        $("#Tab .layui-tab-title>li").each(function (i,item) {
            var html = $(this).html();
            $(this).html(html.replace("短语", CNAME));
        });
        $("legend").each(function (i, item) {
            var html = $(this).html();
            $(this).html(html.replace("短语", CNAME));
        });
        $("#LBPhraseCNameLabel").html(CNAME);
        //暂时只有项目短语维护样本类型，样本短语暂时先不维护样本类型 查询时也不用判断样本类型
        if (OBJECTTYPE == 2) {
            if ($("#SampleTypeBox").hasClass('layui-hide')) $("#SampleTypeBox").removeClass("layui-hide");
        } else {
            if (!$("#SampleTypeBox").hasClass('layui-hide')) $("#SampleTypeBox").addClass("layui-hide");
        }
    };
    //初始化
    function Init() {
        if (!PHRASETYPE || String(PHRASETYPE) == 'null' || !TYPECODE || String(TYPECODE) == 'null') {
            uxbase.MSG.onWarn("缺少必要的参数,请检查!");
            return;
        }
        //初始化HTML
        InitHtml();
        //初始化样本类型
        InitSampleType(function () {
            //初始化列表
            InitTable(function (list) {
                GetCheckBoxByTable(list);
            });
            //初始化监听
            InitListeners();
        });
    };

    //初始化
    Init();
});