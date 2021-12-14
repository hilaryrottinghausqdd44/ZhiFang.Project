/**
   @Name：打印模板使用
   @Author：zhangda
   @version 2021-04-13
*/
//全局变量 -- 参数需要在父级页面赋值  两种模式：一种是数据list，一种是pdf服务地址的参数配置{}
var PrintDataStr = "";
//获取pdf服务地址
var GetPDFUrl = null;
/*打印数据  不要前缀 
 * 
 * --[[{
 *  "Id":"5440192687936741575",
 *  "GTestDate":"2021-03-15",
 *  "GSampleNoForOrder":"00000000000000000007",
 *  "GSampleNo":"7",
 *  "CName":"糖耐量2",
 *  "BarCode":"",
 *  "PatNo":"tnl_2",
 *  "GSampleType":"",
 *  "LisPatient_DeptName":"",
 *  "ItemNameList":"葡萄糖(餐后1小时)"
 *  }]]
 */

layui.extend({
    uxutil: 'ux/util',
    print:'ux/print'
}).use(['uxutil','print', 'table', 'form', 'element', 'laydate'], function () {
    var $ = layui.$,
        element = layui.element,
        laydate = layui.laydate,
        form = layui.form,
        uxutil = layui.uxutil,
        print = layui.print,
        table = layui.table;

    var app = {};
    //配置
    app.config = {
        LABID: null
    };
    //参数
    app.params = {
        BusinessType: null, // 业务类型枚举记录
        ModelType: null, // 模板类型枚举记录Id
        ModelTypeName: null, // 模板类型枚举记录Name
        IsHasExportExcelBtn: false,//是否有导出excel按钮
        isShowPrintInfo: true, //是否存在打印功能
        isDownLoadPDF: false,//打印同时是否下载PDF
        PrinterName: null,//打印机名称
        IsPreview:null //是否预览 1:预览 0:不预览
    };
    //当前登录人历史记录的打印信息
    app.local = {
        print: {
            template: null,
            printer: null,
            preview: false
        }
    };
    //服务地址
    app.url = {
        SELECTURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBPrintModelAndAutoUploadModel?isPlanish=true', //查询 
        PRINTURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_PrintDataByPrintModel', //打印
        EXPORTEXCELURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_ExportDataByPrintModel'//获得Excel路径
    };
    //初始化
    app.init = function () {
        var me = this;
        //获得labid
        me.config.LABID = uxutil.cookie.get(uxutil.cookie.map.LABID);
        me.getParams();
        if (me.params.BusinessType == null || me.params.ModelType == null) {
            layer.msg("缺少必要的参数!", { icon: 0, anim: 0 });
            return;
        }
        //模板类型名称显示
        if (me.params.ModelTypeName != null) $("#ModelTypeName").html(me.params.ModelTypeName);
        //是否显示部分打印信息
        if ((String(me.params.isShowPrintInfo) == "true" || me.params.isShowPrintInfo == 1) && $("#PrintInfo").hasClass("layui-hide")){
			$("#PrintInfo").removeClass("layui-hide");
			$("#print").removeClass("layui-hide");
		}
        else if ((String(me.params.isShowPrintInfo) == "false" || me.params.isShowPrintInfo == 0) && !$("#PrintInfo").hasClass("layui-hide")){
			$("#PrintInfo").addClass("layui-hide");
			$("#print").addClass("layui-hide");	
		}
        //是否显示导出Excel按钮
        if ((String(me.params.IsHasExportExcelBtn) == "true" || me.params.IsHasExportExcelBtn == 1) && $("#export").hasClass("layui-hide"))
            $("#export").removeClass("layui-hide");
        else if ((String(me.params.IsHasExportExcelBtn) == "false" || me.params.IsHasExportExcelBtn == 0) && !$("#export").hasClass("layui-hide"))
            $("#export").addClass("layui-hide");
        me.getlocalPrintInfo();
        me.initPrintTemplate();
        me.initPrinter();
        me.initIsPreview();
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
        //打印
        form.on('submit(print)', function (data) {
            me.print(data.field);
            return false;
        });
        //导出Excel
        $("#export").click(function () {
            me.exportExcel();
        });
        //取消
        //$("#cancel").on('click', function () {
            
        //});
    };
    //获得localStorage存储的打印信息
    app.getlocalPrintInfo = function () {
        var me = this,
            local = uxutil.localStorage.get('template', true);
        if (local && local[me.params.BusinessType] && local[me.params.BusinessType][me.params.ModelType] && local[me.params.BusinessType][me.params.ModelType][me.config.LABID]) {
            var info = local[me.params.BusinessType][me.params.ModelType][me.config.LABID] || {};
            me.local.print.template = info.template;
            me.local.print.printer = info.printer || 0;
            me.local.print.preview = info.preview || false;
        }
        if (me.params.PrinterName) me.local.print.printer = me.params.PrinterName;
    };
    //初始化打印模板
    app.initPrintTemplate = function () {
        var me = this,
            BusinessTypeCode = me.params.BusinessType,
            ModelTypeCode = me.params.ModelType,
            url = me.url.SELECTURL + "&BusinessTypeCode=" + BusinessTypeCode + "&ModelTypeCode=" + ModelTypeCode + '&fields=BPrintModelVO_Id,BPrintModelVO_FileName&sort=[{ "property": "BPrintModel_DispOrder","direction": "ASC" }]';
        var load = layer.load();
        uxutil.server.ajax({
            url: url
        }, function (res) {
                layer.close(load);
                if (res.success && res.ResultDataValue) {
                    var html = "";
                    $.each(res.value.list, function (i, item) {
                        html += '<option value="' + item["BPrintModelVO_Id"] + '">' + item["BPrintModelVO_FileName"] + '</option>';
                    });
                    $("#template").html(html);
                    //如果存在之前记录 则选中
                    if (me.local.print.template != null) $("#template").val(me.local.print.template);
                    form.render('select');
                }
        });
    };
    //初始化打印机
    app.initPrinter = function () {
        var me = this,
            html = "<option value=''>请选择</option>";
        print.instance._initLicenses(function () {
            var printerCount = LODOP.GET_PRINTER_COUNT();
            for (var i = 0; i < printerCount; i++) {
                var name = LODOP.GET_PRINTER_NAME(i);//根据设备序号获取设备名
                //如果存在之前记录 则选中
                if (name == me.local.print.printer)
                    html += '<option value="' + i + '" selected>' + name + '</option>';
                else
                    html += '<option value="' + i + '">' + name + '</option>';
            }
            $("#printer").html(html);
            form.render('select');
        });
    };
    //初始化是否预览
    app.initIsPreview = function () {
        var me = this;
        if (me.params.IsPreview != null) {
            $("#IsPreview").prop('checked', me.params.IsPreview != 0 ? true : false);
            form.render('checkbox');
        }else if (me.local.print.preview != null) {
            $("#IsPreview").prop('checked', me.local.print.preview);
            form.render('checkbox');
        }
    };
    //打印
    app.print = function (data) {
        var me = this,
            template = data["template"],
            printer = data["printer"],
            printerName = $("#printer>option:selected").text(),
            printCount = Number(data["printCount"]),
            IsPreview = $("#IsPreview").prop("checked") ? 1 : 0,
            local = uxutil.localStorage.get('template', true);
        //验证
        if (!template) {
            layer.msg("请选择打印模板!", { icon: 0, anim: 0 });
            return;
        }
        if (printer == "" && printer == null) {
            layer.msg("请选择打印机!", { icon: 0, anim: 0 });
            return;
        }
        if (typeof (PrintDataStr) != 'string') {
            layer.msg("请传递正确的JSON字符串!", { icon: 0, anim: 0 });
            return;
        }
        if (printCount <= 0) {
            layer.msg("请选择一份或一份以上的打印份数!", { icon: 0, anim: 0 });
            return;
        }
        //localStorage存储 template/业务类型Id/模板类型Id/labid/{ 具体属性 }
        if (!local) {
            var info = {};
            info[me.params.BusinessType] = {};
            info[me.params.BusinessType][me.params.ModelType] = {};
            info[me.params.BusinessType][me.params.ModelType][me.config.LABID] = {
                template: template,
                printer: printerName,
                preview: IsPreview
            };
            uxutil.localStorage.set('template', JSON.stringify(info));
        } else {
            if (local[me.params.BusinessType]) {
                if (!local[me.params.BusinessType][me.params.ModelType]) {
                    local[me.params.BusinessType][me.params.ModelType] = {};
                }
            } else {
                local[me.params.BusinessType] = {};
                local[me.params.BusinessType][me.params.ModelType] = {};
            }
            //设置labid对应打印属性
            local[me.params.BusinessType][me.params.ModelType][me.config.LABID] = {
                template: template,
                printer: printerName,
                preview: IsPreview
            };
            uxutil.localStorage.set('template', JSON.stringify(local));
        }
        //获得PDF
        var load = layer.load();
        var config = {};
        if (GetPDFUrl) {//存在获得pdf的服务地址
            var PrintData = JSON.parse(PrintDataStr);//PrintDataStr 传递参数对象（用字符串传递）
            PrintData["modelcode"] = template;//模板参数赋值 参数名必须为modelcode
            config = {
                type: "POST",
                url: GetPDFUrl,
                data: JSON.stringify(PrintData)
            };
        } else {
            config = {
                type: "POST",
                url: me.url.PRINTURL,
                data: JSON.stringify({ Data: PrintDataStr, PrintModelID: template })
            };
        }
        uxutil.server.ajax(config, function (res) {
            layer.close(load);
            if (res.success) {
                var pdfurl = uxutil.path.ROOT + "/" + res.ResultDataValue;
                if (me.params.isDownLoadPDF && String(me.params.isDownLoadPDF) != "false" && me.params.isDownLoadPDF != 0)
                    window.location = pdfurl;
                if (IsPreview) {
                    print.instance.pdf.preview([pdfurl], me.params.ModelTypeName, function () { }, true, function () {
                        if (!isNaN(printCount) && printCount > 0) LODOP.SET_PRINT_COPIES(printCount);//设置打印份数
                        if (printer != 'undefined' && !isNaN(printer)) LODOP.SET_PRINTER_INDEX(printer);//设置打印机
                        //预览界面是否允许操作
                        if (true) {
                            LODOP.SET_PRINT_MODE("RESELECT_PRINTER", true); //允许重选打印机
                            LODOP.SET_PRINT_MODE("RESELECT_ORIENT", true); //允许重选纸张方向
                            LODOP.SET_PRINT_MODE("RESELECT_PAGESIZE", true); //允许重选纸张
                            LODOP.SET_PRINT_MODE("RESELECT_COPIES", true); //允许重选份数
                        }
                    });
                } else {
                    print.instance.pdf.print([pdfurl], me.params.ModelTypeName, function () { }, true, function () {
                        if (!isNaN(printCount) && printCount > 0) LODOP.SET_PRINT_COPIES(printCount);//设置打印份数
                        if (printer != 'undefined' && !isNaN(printer)) LODOP.SET_PRINTER_INDEX(printer);//设置打印机
                    });
                }
            } else {
                layer.msg("获得PDF文件失败!",{ icon: 5, anim: 6 });
            }
        })
    };
    //导出excel
    app.exportExcel = function () {
        var me = this,
            template = $("#template").val(),
            local = uxutil.localStorage.get('template', true);
        //验证
        if (!template) {
            layer.msg("请选择打印模板!", { icon: 0, anim: 0 });
            return;
        }
        if (typeof (PrintDataStr) != 'string') {
            layer.msg("请传递正确的JSON字符串!", { icon: 0, anim: 0 });
            return;
        }
        //localStorage存储 template/业务类型Id/模板类型Id/labid/{ 具体属性 }
        if (!local) {
            var info = {};
            info[me.params.BusinessType] = {};
            info[me.params.BusinessType][me.params.ModelType] = {};
            info[me.params.BusinessType][me.params.ModelType][me.config.LABID] = {
                template: template
            };
            uxutil.localStorage.set('template', JSON.stringify(info));
        } else {
            if (local[me.params.BusinessType]) {
                if (!local[me.params.BusinessType][me.params.ModelType]) {
                    local[me.params.BusinessType][me.params.ModelType] = {};
                }
            } else {
                local[me.params.BusinessType] = {};
                local[me.params.BusinessType][me.params.ModelType] = {};
            }
            //设置labid对应打印属性
            local[me.params.BusinessType][me.params.ModelType][me.config.LABID] = {
                template: template
            };
            uxutil.localStorage.set('template', JSON.stringify(local));
        }
        //获得Excel
        var load = layer.load();
        var config = {
            type: "POST",
            url: me.url.EXPORTEXCELURL,
            data: JSON.stringify({ Data: PrintDataStr, PrintModelID: template })
        };
        uxutil.server.ajax(config, function (res) {
            layer.close(load);
            if (res.success) {
                var url = uxutil.path.ROOT + "/" + res.ResultDataValue;
                window.location.href = url;
            } else {
                layer.msg("获得Excel文件路径失败!", { icon: 5, anim: 6 });
            }
        })
    };
    //表单验证
    form.verify({
        ZDY_required: function (value, item) { //value：表单的值、item：表单的DOM对象
            if (value == "") {
                var label = $(item).parents(".layui-form-item").children(".layui-form-label").html(),
                    msg = "";
                if (label) {
                    msg = label + "不能为空！";
                } else {
                    msg = '必填项不能为空';
                }
                return msg;
            }
        },
        ZDY_number: function (value, item) {
            if (!value || isNaN(value)) {
                var label = $(item).parents(".layui-form-item").children(".layui-form-label").html(),
                    msg = "";
                if (label) {
                    msg = label + "只能填写数字！";
                } else {
                    msg = '只能填写数字';
                }
                return msg;
            }
        }
    }); 
    //初始化
    app.init();
});