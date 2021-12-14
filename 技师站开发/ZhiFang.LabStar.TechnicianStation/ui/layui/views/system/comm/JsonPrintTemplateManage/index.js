/**
   @Name：打印模板
   @Author：zhangda
   @version 2021-04-08
 */
layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'table', 'form', 'element', 'laydate'], function () {
    var $ = layui.$,
        element = layui.element,
        laydate = layui.laydate,
        form = layui.form,
        uxutil = layui.uxutil,
        table = layui.table;

    var app = {};
    //参数
    app.params = {
        type: "1",        // 1:单独使用，2：外部调用
        BusinessType: null, // 业务类型枚举记录Id
        ModelType: null, // 模板类型枚举记录Id
    };
    //配置
    app.config = {
        loadIndex: null
    };
    //服务地址
    app.url = {
        GETMAXNOBYENTITYFIELDURL: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField',//获取指定实体字段的最大号
        GETENUMTYPEURL: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic',   //获得枚举 传递枚举类型名 
        SELECTURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBPrintModelAndAutoUploadModel?isPlanish=true', //查询 
        UPLOADURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UploadReportModel',   //上传文件并新增数据 
        EDITURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateReportModel',   //编辑
        DELURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelReportModelById',   //删除
        EDITBASETABLEURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateBPrintModelByField',   //编辑基础服务-用于修改显示次序
    };
    //查询字段
    app.fields = 'BPrintModelVO_Id,BPrintModelVO_BusinessTypeCode,BPrintModelVO_BusinessTypeName,BPrintModelVO_ModelTypeCode,BPrintModelVO_ModelTypeName,BPrintModelVO_FileName,BPrintModelVO_FileComment,BPrintModelVO_OperaterID,BPrintModelVO_Operater,BPrintModelVO_FinalOperaterID,BPrintModelVO_FinalOperater,BPrintModelVO_IsProtect,BPrintModelVO_IsUse,BPrintModelVO_FileUploadTime,BPrintModelVO_UploadComputer,BPrintModelVO_DispOrder,BPrintModelVO_IsFile';
    //初始化
    app.init = function () {
        var me = this;
        me.config.loadIndex = layer.load();
        me.getParams();
        if (me.params.type == '2' && (me.params.BusinessType == null || me.params.ModelType == null)) {
            layer.msg("缺少必要的参数!");
            return;
        }
        me.initSelect();
        me.initMainTable();
        me.initListeners();
    };
    //获得参数
    app.getParams = function () {
        var me = this,
            params = uxutil.params.get();
        me.params = $.extend({}, me.params, params);
    };
    //监听
    app.initListeners = function () {
        var me = this;
        //监听表格行工具事件
        table.on('tool(mainTable)', function (obj) {
            var data = obj.data, //获得当前行数据
                layEvent = obj.event,
                IsProtect = String(data["BPrintModelVO_IsProtect"]) == 'true' ? true : false,//是否加强保护
                Id = data["BPrintModelVO_Id"],
                UserID = uxutil.cookie.get(uxutil.cookie.map.USERID);
            if (layEvent === 'download') {//下载
                var labid = uxutil.cookie.get(uxutil.cookie.map.LABID);
                var url = uxutil.path.ROOT + '/Template/' + data["BPrintModelVO_BusinessTypeName"] + '/' + data["BPrintModelVO_ModelTypeName"] + '/' + labid + '/' + data["BPrintModelVO_FileName"];
                window.location = url;
                
            } else if (layEvent === 'edit') { //编辑
                if (IsProtect && UserID != data["BPrintModelVO_OperaterID"]) {
                    layer.msg("该模板为加强保护文件，只允许上传者更改!");
                    return;
                }
                //重置表单状态
                me.reset();
                var BusinessType = $("#BusinessType option:selected"),
                    ModelType = $("#ModelType option:selected");
                //赋值表单
                $("#Id").val(data["BPrintModelVO_Id"]);
                $("#DispOrder").val(data["BPrintModelVO_DispOrder"]);
                $("#FileComment").val(data["BPrintModelVO_FileComment"]);
                $("#IsProtect").prop("checked", IsProtect);
                $("#IsUse").prop("checked", (String(data["BPrintModelVO_IsUse"]) == 'true' ? true : false));
                form.render("checkbox");//重新渲染页面checkbox控件
                layer.open({
                    type: 1,
                    title: '修改打印模板【 <span style="color:red;">' + data["BPrintModelVO_FileName"]+'</span> 】' + " - 【" + BusinessType.text() + " > " + ModelType.text() + "】",
                    area: ['500px', '400px'],
                    content: $("#modal"),
                    btn: ['保存'],
                    yes: function (index, layero) {
                        me.save('eidt',0);
                    }
                });
            } else if (layEvent === 'del') { //删除
                if (IsProtect && UserID != data["BPrintModelVO_OperaterID"]) {
                    layer.msg("该模板为加强保护文件，只允许上传者更改!");
                    return;
                }
                layer.confirm('是否确认删除?', { icon: 3, title: '提示' }, function (index) {
                    me.del(Id);
                });
            } else if (layEvent === 'up') { //上移
                me.move(Id, layEvent);
            } else if (layEvent === 'down') { //下移
                me.move(Id, layEvent);
            }
        });
        //上传
        $("#upload").on("click", function () {
            var BusinessType = $("#BusinessType option:selected"),
                ModelType = $("#ModelType option:selected");
            //重置表单状态
            me.reset();
            //初始化显示次序
            me.getMaxNoByEntityField('BPrintModel', 'DispOrder', function (value) { $("#DispOrder").val(value); });
            layer.open({
                type: 1,
                title: '上传打印模板' + " - 【" + BusinessType.text() + " > " + ModelType.text()+"】",
                area: ['500px', '400px'],
                content: $("#modal"),
                btn: ['保存'],
                yes: function (index, layero) {
                    //按钮【按钮一】的回调
                    me.save('add',0);
                }
            });
        });
        //监听业务类型切换
        form.on('select(BusinessType)', function (data) {
            me.onSearch();
        });
        //监听模板类型切换
        form.on('select(ModelType)', function (data) {
            me.onSearch();
        });    
    };
    //初始化样本单列表
    app.initMainTable = function () {
        var me = this,
            BusinessTypeCode = $("#BusinessType").val(),
            ModelTypeCode = $("#ModelType").val(),
            url = me.url.SELECTURL + "&BusinessTypeCode=" + BusinessTypeCode + "&ModelTypeCode=" + ModelTypeCode +
                '&fields=' + me.fields +
                '&sort=[{ "property": "BPrintModel_DispOrder","direction": "ASC" }]';
        
        var tableIns = table.render({
            elem: '#mainTable',
            height: 'full-70',
            url: url,
            toolbar: false,
            defaultToolbar: ["filter", "exports", "print"],
            title:'123',
            page: false,
            limit: 999,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号' },
                { field: 'BPrintModelVO_Id', width: 200, title: '打印模版ID', sort: false, hide: true },
                { field: 'BPrintModelVO_BusinessTypeCode', width: 200, title: '业务类型编码', sort: false, hide: true },
                { field: 'BPrintModelVO_BusinessTypeName', width: 200, title: '业务类型', sort: false, hide: true },
                { field: 'BPrintModelVO_ModelTypeCode', width: 200, title: '模板类型编码', sort: false, hide: true },
                { field: 'BPrintModelVO_ModelTypeName', width: 200, title: '模板类型', sort: false, hide: true },
                { field: 'BPrintModelVO_FileName', width: 200, title: '文件名称', sort: false },
                { field: 'BPrintModelVO_FileComment', minWidth: 160, title: '文件说明', sort: false },
                { field: 'BPrintModelVO_OperaterID', width: 100, title: '上传文件操作者ID', sort: false, hide:true },
                { field: 'BPrintModelVO_Operater', width: 100, title: '上传文件操作者', sort: false, hide: true },
                { field: 'BPrintModelVO_FinalOperaterID', width: 100, title: '最终操作者ID', sort: false, hide: true  },
                { field: 'BPrintModelVO_FinalOperater', width: 100, title: '最终操作者', sort: false, hide: true  },
                {
                    field: 'BPrintModelVO_IsProtect', width: 80, title: '加强保护', sort: false, templet: function (data) {
                        var str = "";
                        if (String(data["BPrintModelVO_IsProtect"]) == "true")
                            str = "<span style='color:red;'>是</span>";
                        else
                            str = "<span>否</span>";
                        return str;
                    }
                },
                {
                    field: 'BPrintModelVO_IsUse', width: 80, title: '在用', sort: false, templet: function (data) {
                        var str = "";
                        if (String(data["BPrintModelVO_IsUse"]) == "true")
                            str = "<span>是</span>";
                        else
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                },
                {
                    field: 'BPrintModelVO_IsFile', width: 130, title: '物理文件是否存在', sort: false, templet: function (data) {
                        var str = "";
                        if (String(data["BPrintModelVO_IsFile"]) == "true")
                            str = "<span>是</span>";
                        else
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                },
                {
                    field: 'BPrintModelVO_FileUploadTime', width: 150, title: '文件上传时间', sort: false,
                    templet: function (data) {
                        var val = data.BPrintModelVO_FileUploadTime.replace(RegExp("/", "g"), "-");
                        return val;
                    }
                },
                { field: 'BPrintModelVO_UploadComputer', width: 100, title: '文件上传站点', sort: false, hide: true },
                { field: 'BPrintModelVO_DispOrder', width: 80, title: '显示次序', sort: false },
                { title: '操作', width: 280, align: 'center', toolbar: '#toolbar' }
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue).list : [];
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.length || 0, //解析数据长度
                    "data": data || []
                };
            },
            done: function (res, curr, count) {
                if (app.config.loadIndex) layer.close(app.config.loadIndex);
                if (count == 0) return;
                if ($("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0])
                    $("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
            }
        });
    };
    //列表刷新
    app.onSearch = function (BusinessType, ModelType) {
        var me = this,
            BusinessTypeCode = BusinessType != null && BusinessType != 'undefined' ? BusinessType : $("#BusinessType option:selected").val(),
            ModelTypeCode = ModelType != null && ModelType != 'undefined' ? ModelType : $("#ModelType option:selected").val();
        table.reload('mainTable', {
            url: me.url.SELECTURL + "&BusinessTypeCode=" + BusinessTypeCode + "&ModelTypeCode=" + ModelTypeCode + '&fields=' + me.fields + '&sort=[{ "property": "BPrintModel_DispOrder","direction": "ASC" }]',
            where: {
                t: new Date().getTime()
            }
        });
    };
    //初始化下拉框
    app.initSelect = function () {
        var me = this;
        //获得打印模板_类型名称
        me.getEnumList('PrintModelBusinessType', function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    var html = "";
                    $.each(data, function (i, item) {
                        html += '<option value="' + item["Id"] + '">' + item["Name"] + '</option>';
                    });
                    $("#BusinessType").html(html);
                    if (me.params.type == '2' && me.params.BusinessType != null) {
                        $("#BusinessType").val(me.params.BusinessType);
                        $("#BusinessType").prop("disabled", "disabled");
                        $("#BusinessType").addClass("layui-disabled");
                    }
                    form.render('select');
                }
            } else {
                layer.msg("打印模板_类型名称枚举查询失败！", { icon: 5, anim: 6 });
            }
        });
        //获得打印模板_模板类型
        me.getEnumList('PrintModelModelType', function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    var html = "";
                    $.each(data, function (i, item) {
                        html += '<option value="' + item["Id"] + '">' + item["Name"] + '</option>';
                    });
                    $("#ModelType").html(html);
                    if (me.params.type == '2' && me.params.ModelType != null) {
                        $("#ModelType").val(me.params.ModelType);
                        $("#ModelType").prop("disabled", "disabled");
                        $("#ModelType").addClass("layui-disabled");
                    }
                    form.render('select');
                }
            } else {
                layer.msg("打印模板_类型名称枚举查询失败！", { icon: 5, anim: 6 });
            }
        });
    };
    //获取类型列表
    app.getEnumList = function (classname, callback) {
        var me = this,
            url = me.url.GETENUMTYPEURL + '?classnamespace=ZhiFang.Entity.LabStar&classname=' + classname;

        uxutil.server.ajax({
            url: url, async: false
        }, function (data) {
            if (typeof callback == 'function') callback(data);
        });
    };
    //获取指定实体字段的最大号
    app.getMaxNoByEntityField = function (entityName, entityField,callback) {
        var me = this,
            url = me.url.GETMAXNOBYENTITYFIELDURL + '?entityName=' + entityName + '&entityField=' + entityField;
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    if (typeof callback == 'function') callback(res.ResultDataValue);
                }
            } else {
                layer.msg("获取指定实体字段的最大号失败！", { icon: 5, anim: 6 });
            }
        });
    }
    //保存
    app.save = function (type,IsConstraintUpload) {
        var me = this,
            type = type || 'add',
            msg = me.verify(type);
        if (msg != "") {
            layer.msg(msg);
            return;
        }
        var load = layer.load();
        $("#IsConstraintUpload").val(IsConstraintUpload ? 1 : 0);
        var url = type == "add" ? me.url.UPLOADURL : me.url.EDITURL,
            //formData = new FormData($("#uploadForm")[0]),
            formData = new FormData(),
            BusinessTypeCode = $("#BusinessType").val(),
            ModelTypeCode = $("#ModelType").val(),
            file = $("#file").val(),
            Id = $("#Id").val(),
            DispOrder = $("#DispOrder").val(),
            FileComment = $("#FileComment").val(),
            IsConstraintUpload = $("#IsConstraintUpload").val(),
            IsProtect = $("#IsProtect").prop("checked"),
            IsUse = $("#IsUse").prop("checked");
        //组装formData
        if (file) formData.append('file', $('#file')[0].files[0]);
        if (Id) formData.append('Id', Id);
        formData.append('DispOrder', DispOrder);
        formData.append('FileComment', FileComment);
        formData.append('IsConstraintUpload', IsConstraintUpload);
        formData.append('BusinessTypeCode', BusinessTypeCode);
        formData.append('ModelTypeCode', ModelTypeCode);
        if (file) formData.append('FileName', file.substr(file.lastIndexOf("\\") + 1));
        formData.append('IsProtect', IsProtect ? 1 : 0);
        formData.append('IsUse', IsUse ? 1 : 0);

        uxutil.server.ajax({
            url: url, type: 'post', data: formData, cache: false, dataType: 'json', processData: false, contentType: false
        }, function (res) {
                layer.close(load);
            if (res.success) {
                layer.closeAll();
                me.onSearch();
                layer.msg("保存成功！", { icon: 6, anim: 0 });
            } else {
                if (res.ErrorInfo == '同名文件已存在') {
                    layer.confirm('同名文件已存在,系统将重新更改模板文件名称，是否继续?', { icon: 3, title: '提示' }, function (index) {
                        me.save(type,1);
                    });
                } else {
                    layer.msg("保存失败！", { icon: 5, anim: 6 });
                }
            }
        });
        return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
    };
    //验证上传内容
    app.verify = function (type) {
        var me = this,
            type = type || 'add',
            ModelTypeName = $("#ModelType>option:selected").text(),
            DispOrder = $("#DispOrder").val(),
            file = $("#file").val();
        //验证显示次序
        if (DispOrder == null || DispOrder == "" || isNaN(DispOrder)) return "显示次序必须是数字!";
        //验证文件
        if (type == 'add' && (file == null || file == "")) return "请选择.frx文件!";
        if (file != "" && !file.indexOf(".frx")) return "请选择.frx文件!";
        if (file != "" && file.substr(file.lastIndexOf("\\") + 1).indexOf(ModelTypeName)) return "文件名前缀未包含模板类型名称!";
        return "";
    };
    //删除
    app.del = function (Id) {
        var me = this,
            Id = Id,
            url = me.url.DELURL + "?Id=" + Id;
        if (!Id) return;
        var load = layer.load();
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data.success === true) {
                layer.closeAll('loading');//隐藏遮罩层
                me.onSearch();
                layer.msg("删除成功!");
            } else {
                layer.msg("删除失败!");
            }
        });
    };
    //移动
    app.move = function (Id,type) {
        var me = this,
            Id = Id,
            url = me.url.EDITBASETABLEURL,
            type = type || 'up', // up:上移，down：下移
            LAY_TABLE_INDEX = null,//记录当前行下标
            data = [],//发送数据
            saveCount = 0,
            saveSuccessCount = 0,
            saveErrorCount = 0,
            tableCache = table.cache["mainTable"];
        if (!Id) return;
        $.each(tableCache, function (i,item) {
            if (Id == item["BPrintModelVO_Id"]) {
                LAY_TABLE_INDEX = item["LAY_TABLE_INDEX"];
                return false;
            }
        });
        if (LAY_TABLE_INDEX == null) return;
        if (type == 'up') {
            if (LAY_TABLE_INDEX == 0) {
                layer.msg("已经是第一条!");
                return;
            }
            data.push({ Id: Id, DispOrder: tableCache[LAY_TABLE_INDEX - 1]["BPrintModelVO_DispOrder"] });
            data.push({ Id: tableCache[LAY_TABLE_INDEX - 1]["BPrintModelVO_Id"], DispOrder: tableCache[LAY_TABLE_INDEX]["BPrintModelVO_DispOrder"] });
        } else if (type == 'down') {
            if (LAY_TABLE_INDEX == tableCache.length - 1) {
                layer.msg("已经是最后一条!");
                return;
            }
            data.push({ Id: Id, DispOrder: tableCache[LAY_TABLE_INDEX + 1]["BPrintModelVO_DispOrder"] });
            data.push({ Id: tableCache[LAY_TABLE_INDEX + 1]["BPrintModelVO_Id"], DispOrder: tableCache[LAY_TABLE_INDEX]["BPrintModelVO_DispOrder"] });
        }
        saveCount = data.length;
        var load = layer.load();
        $.each(data, function (i, item) {
            var entity = entity = {
                entity: {
                    Id: item["Id"],
                    DispOrder: item["DispOrder"]
                },
                fields: "Id,DispOrder"
            };
            var config = {
                type: "POST",
                url: url,
                data: JSON.stringify(entity)
            };
            uxutil.server.ajax(config, function (data) {
                if (data.success) {
                    saveSuccessCount++;
                } else {
                    saveErrorCount++;
                }
                if (saveSuccessCount + saveErrorCount == saveCount) {
                    layer.close(load);
                    me.onSearch();
                }
            })
        });
    };
    //重置表单状态
    app.reset = function () {
        var me = this;
        $("#uploadForm")[0].reset();
        //document.getElementById('file').value = '';
    };
    //初始化
    app.init();
});