/**
	@name：图形结果
	@author：zhangda
	@version 2021-05-18
 */
layui.extend({
}).define(['form', 'uxutil','uxbase'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        uxbase = layui.uxbase,
        form = layui.form;

    var app = {};

    app.config = {
        testFormRecord: null, isReadOnly: true, sectionID: null,
        FIELDS: ['Id', 'GraphNo', 'GraphName', 'GraphType', 'GraphDataID', 'GraphInfo', 'GraphComment', 'GraphHeight', 'GraphWidth', 'DispOrder', 'IsReport'],
        imageRightClickInfo: { testGraphID: null, graphDataID: null, graphDispOrder: null, graphName: null, graphElem: null },
        //需要生成缩略图的宽高 大于 则等比例去生成
        imgWidth: 200,
        imgHeight: 200,
        //新增图形弹出层index
        addImagelayerIndex: null
    };
    app.url = {
        //获取数据服务路径
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestGraphByHQL?isPlanish=true',
        //获取LIS图形库图形结果表数据
        getImageInfoUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisGraphData',
        //编辑数据服务路径
        editUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestGraphByField',
        /**删除数据服务路径*/
        delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_DelLisTestGraphData',
        //检验结果图形表数据保存
        addLisTestGraphDataUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_AddLisTestGraphData'
    };

    //核心入口
    app.render = function (options) {
        var me = this;
        me.config = $.extend({}, me.config, options);
        //只读处理
        if (me.config.isReadOnly) {
            if (!$("#addImageBox").hasClass("layui-hide")) $("#addImageBox").addClass("layui-hide");
        } else {
            if ($("#addImageBox").hasClass("layui-hide")) $("#addImageBox").removeClass("layui-hide");
        }
        me.iniListeners();
        return me;
    };
    //查询
    app.search = function (options) {
        var me = this,
            msg = "<div style='text-align:center;color:red;'>没有找到图片</div>";
        me.config = $.extend({}, me.config, options);
        if (!me.config.testFormRecord) {
            me.clearData(msg);
            return;
        } else {
            me.getImagesListByTestForm(function (list) {
                if (list && list.length > 0) {
                    me.clearData();
                    me.createImage(list);
                } else {
                    me.clearData(msg);
                }
            });
        }
    };
    //监听
    app.iniListeners = function () {
        var me = this;
        //新增图形结果
        $("#addImage").on("click", function (e) {
            if (me.config.isReadOnly) return;
            if (!me.config.testFormRecord) {
                uxbase.MSG.onWarn("请选择一条检验单进行新增图形结果!");
                return;
            }
            me.config.addImagelayerIndex = layer.open({
                title: "新增图形",
                type: 1,
                content: $("#addImageModal"),
                maxmin: true,
                resize: true,
                area: ['400px', '220px'],
                success: function (layero, index) {
                    //清空
                    $("#addImageForm :input").each(function () {
                        if ($(this).attr("type") == 'checkbox')
                            $(this).prop('checked', true);
                        else
                            $(this).val('');
                    });
                    //设置图形默认信息
                    var disOrder = 1;
                    if ($("#imagesContent img").length > 0) {//已存在img
                        disOrder = Number($("#imagesContent img:last").attr("data-disorder")) + 1;
                    }
                    $("#graphName").val("图形" + disOrder);
                    $("#dispOrder").val(disOrder);
                }
            });
        });
        //保存图形结果按钮
        form.on('submit(saveImage)', function (data) {
            var file = $("#imgFile")[0].files[0];
            //图形扩展名
            var imgType = file.type;
            //转为base64
            me.blobToDataURL(file, function (result) {
                me.saveImage(result, imgType, data.field);
            });
            return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
        });
        //监听图形双击
        $("#imagesContent").on('dblclick', 'img', function () {
            var graphDataID = $(this).attr("data-graphdataid"),
                graphName = $(this).attr("data-name"),
                graphWidth = $(this).attr("data-width"),
                graphHeight = $(this).attr("data-height"),
                maxWidth = $(window).width() - 20,
                maxHeight = $(window).height() - 20,
                width = graphWidth > maxWidth ? '90%' : (Number(graphWidth) < 300 ? 300 : graphWidth) + "px",
                height = graphHeight > maxHeight ? '90%' : (Number(graphHeight) + 50) + "px";

            me.getImageInfo(1, graphDataID, function (res) {
                if (res.success) {
                    var imageSrc = res.value;
                    parent.layer.open({
                        title: "图形结果：" + graphName,
                        type: 1,
                        content: '<img src="' + imageSrc + '" width="' + graphWidth + '" height="' + graphHeight + '" />',
                        maxmin: true,
                        resize: true,
                        area: [width, height],
                        success: function (layero, index) { },
                        end: function () { }
                    });
                } else {
                    uxbase.MSG.onError(res.msg);
                }
            });
        });
        //鼠标右键阻止菜单
        $("#imagesContent").on("contextmenu", 'img', function () { return false; });
        //鼠标右键阻止菜单
        $("#imageRightClickMenu").on("contextmenu", function () { return false; });
        //监听图形右键
        $("#imagesContent").on('mousedown', 'img', function () {
            var e = e || window.event,//e.which: 1 = 鼠标左键 left; 2 = 鼠标中键; 3 = 鼠标右键
                testGraphID = $(this).attr("data-id"),//图形ID
                graphDataID = $(this).attr("data-graphdataid"),//图形ID
                graphDispOrder = $(this).attr("data-disorder"),//图形排序序号
                graphName = $(this).attr("data-name"),
                graphElem = $(this);//图形名称
            if (e.which == 3 && !me.config.isReadOnly) {
                if (e.target.tagName == "IMG") {//在质控图处右击
                    var _x = e.clientX - parseFloat($("#imageRightClickMenu").css("width")),
                        _y = e.clientY + me.getScrollTop() - parseFloat($("#imageRightClickMenu").css("height"));
                    //记录当前img信息
                    me.config.imageRightClickInfo.testGraphID = testGraphID;
                    me.config.imageRightClickInfo.graphDataID = graphDataID;
                    me.config.imageRightClickInfo.graphDispOrder = graphDispOrder;
                    me.config.imageRightClickInfo.graphName = graphName;
                    me.config.imageRightClickInfo.graphElem = graphElem;
                    //右键菜单显示
                    $("#ImageName").html(graphName);
                    $("#imageRightClickMenu").css({ "display": "block", "left": _x + "px", "top": _y + "px" });
                }
            }
        });
        //监听右键操作
        $("#imageRightClickMenu").on('click', 'li', function () {
            var dataid = $(this).attr("data-id");
            switch (dataid) {
                case "delImage":
                    me.delImage();
                    break;
                case "moveUpImage":
                    me.moveUpImage();
                    break;
                case "moveDownImage":
                    me.moveDownImage();
                    break;
                case "editImageName":
                    me.editImageName();
                    break;
                default:
                    break;
            }
            if ($("#imageRightClickMenu").css("display") != "none") $("#imageRightClickMenu").css("display", "none");
        });
        //点击其他位置关闭菜单
        $(document).on('click', function (e) {
            $("#imageRightClickMenu").css("display", "none");
        });
        //监听获得焦点
        $("#pasteImage").on('focus', function () {
            $(this).html("");
        });
        //监听失去焦点
        $("#pasteImage").on('blur', function () {
            $(this).html("在此可粘贴图片<br>自动保存");
        });
        //监听结果图片的粘贴操作  
        document.getElementById('pasteImage').addEventListener('paste', function (event) {
            me.handlePaste(event);
            return false;
        });
    };
    //删除图形
    app.delImage = function () {
        var me = this,
            imageRightClickInfo = me.config.imageRightClickInfo,
            testGraphID = me.config.imageRightClickInfo.testGraphID,
            graphElem = imageRightClickInfo.graphElem,
            url = me.url.delUrl;
        if (!testGraphID) return;
        url += "?id=" + testGraphID;
        var load = layer.load();
        uxutil.server.ajax({
            url: url
        }, function (res) {
            layer.close(load);
            if (res.success) {
                $(graphElem).remove();
                uxbase.MSG.onSuccess("删除成功!");
            } else {
                uxbase.MSG.onError("删除失败!");
            }
        });
    };
    //上移图形
    app.moveUpImage = function () {
        var me = this,
            imageRightClickInfo = me.config.imageRightClickInfo,
            testGraphID = me.config.imageRightClickInfo.testGraphID,
            graphDispOrder = me.config.imageRightClickInfo.graphDispOrder,
            graphElem = imageRightClickInfo.graphElem,
            index = $(graphElem).index(),//处于父元素的下标
            prevElem = $("#imagesContent img:eq(" + (index - 1) + ")"),
            list = [{ Id: testGraphID, DispOrder: $(prevElem).attr("data-disorder") }, { Id: $(prevElem).attr("data-id"), DispOrder: graphDispOrder }];
        if (index == 0 || prevElem.length == 0) {
            uxbase.MSG.onWarn("已经是第一个图形!");
            return;
        }
        me.editImageDisOrder(list, function () {
            $(graphElem).insertBefore($(prevElem));
        });
    };
    //下移图形
    app.moveDownImage = function () {
        var me = this,
            imageRightClickInfo = me.config.imageRightClickInfo,
            testGraphID = me.config.imageRightClickInfo.testGraphID,
            graphDispOrder = me.config.imageRightClickInfo.graphDispOrder,
            graphElem = imageRightClickInfo.graphElem,
            index = $(graphElem).index(),//处于父元素的下标
            nextElem = $("#imagesContent img:eq(" + (index + 1) + ")"),
            list = [{ Id: testGraphID, DispOrder: $(nextElem).attr("data-disorder") }, { Id: $(nextElem).attr("data-id"), DispOrder: graphDispOrder }];
        if (nextElem.length == 0) {
            uxbase.MSG.onWarn("已经是最后一个图形!");
            return;
        }
        me.editImageDisOrder(list, function () {
            $(nextElem).insertBefore($(graphElem));
        });
    };
    //修改图形名称
    app.editImageName = function () {
        var me = this,
            imageRightClickInfo = me.config.imageRightClickInfo,
            testGraphID = me.config.imageRightClickInfo.testGraphID,
            graphElem = imageRightClickInfo.graphElem,
            params = {},
            url = me.url.editUrl;
        //弹出名称修改框
        layer.open({
            title: "修改图形名称",
            type: 1,
            content: '<div class="layui-form" style="padding:5px;"><input type="text" style="height: 38px!important;" placeholder="图形名称" autocomplete="off" id="GraphNameInput" class="layui-input" /></div>',
            btn: ['确定', '取消'],
            yes: function (index, layero) {//确定回调
                var GraphName = $("#GraphNameInput").val();
                if (GraphName == "") {
                    uxbase.MSG.onWarn("图形名称不能为空!");
                    return;
                }
                params.entity = { Id: testGraphID, GraphName: GraphName };
                params.fields = 'Id,GraphName';
                var load = layer.load();
                uxutil.server.ajax({
                    url: url,
                    type: 'post',
                    data: JSON.stringify(params)
                }, function (res) {
                    layer.close(load);
                    if (res.success) {
                        layer.close(index);
                        $(graphElem).attr("data-name", GraphName).attr("title", GraphName);
                        uxbase.MSG.onSuccess("图形名称修改成功!");
                    } else {
                        uxbase.MSG.onError(res.ErrorInfo || res.msg);
                    }
                });
            },
            btn2: function (index) {//取消回调
                layer.close(index);
            },
            maxmin: true,
            resize: true,
            area: ['300px', '150px']
        });
    };
    //修改图形排序
    app.editImageDisOrder = function (list, callBack) { // [{Id:1,DispOrder:1}]
        var me = this,
            saveCount = list.length,
            saveSuccessCount = 0,
            saveErrorCount = 0,
            url = me.url.editUrl;
        if (list.length == 0) return;
        
        var load = layer.load();
        $.each(list, function (i, item) {
            var Id = item["Id"],
                DispOrder = item["DispOrder"],
                params = {};
            params.entity = { Id: Id, DispOrder: DispOrder };
            params.fields = 'Id,DispOrder';
            setTimeout(function () {
                uxutil.server.ajax({
                    url: url,
                    type: 'post',
                    data: JSON.stringify(params)
                }, function (res) {
                    if (res.success)
                        saveSuccessCount++;
                    else
                        saveSuccessCount--;
                    if (saveSuccessCount + saveErrorCount == saveCount) {
                        layer.close(load);
                        if (saveErrorCount != 0)
                            uxbase.MSG.onError("存在失败记录!");
                        else {
                            uxbase.MSG.onSuccess("更新成功!");
                            //交换元素位置
                            callBack();
                        }
                    }
                });
            }, 100 * i);
        });
    };
    //获取检验图形列表
    app.getImagesListByTestForm = function (callback) {
        var me = this,
            testFormRecord = me.config.testFormRecord,
            url = me.url.selectUrl;

        url += '&fields=' + "LisTestGraph_" + me.config.FIELDS.join(",LisTestGraph_");
        url += '&where=listestgraph.LisTestForm.Id=' + testFormRecord['LisTestForm_Id'];
        url += '&sort=' + JSON.stringify([{ "property": "LisTestGraph_DispOrder", "direction": "asc" }]);
        url += '&t=' + new Date().getTime();

        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                var list = (res.value || {}).list || [];
                callback(list);
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };
    //获取图形信息
    app.getImageInfo = function (graphSizeType, graphDataID, callback) {
        var me = this,
            url = me.url.getImageInfoUrl;

        url += '?graphSizeType=' + graphSizeType + '&graphDataID=' + (graphDataID ? graphDataID : 0);
        uxutil.server.ajax({
            url: url
        }, function (res) {
            callback(res);
        });
    };
    //保存图形
    app.saveImage = function (imgBase64, type, data) {
        var me = this,
            imgBase64 = imgBase64 || null,
            type = type || 'image/png',
            imgType = type.split("/")[1] || 'png',
            data = data || {},
            isReport = (data["isReport"] == 1 || data["isReport"] == 0) ? data["isReport"] : $("#isReport").prop("checked"),
            formData = new FormData();
        if (!imgBase64) return;
        if (!me.config.testFormRecord) {
            uxbase.MSG.onWarn("请先选择检验单!");
            return;
        };
        if (type.indexOf('image') == -1) {
            uxbase.MSG.onWarn("请选择图片文件!");
            return;
        };
        var loadIndex = layer.load();
        //formData值
        formData.append('testFormID', me.config.testFormRecord["LisTestForm_Id"]);//样本单ID
        formData.append('graphBase64', imgBase64);//原图片base64
        formData.append('graphName', data["graphName"]);//图形名称
        formData.append('graphType', imgType);//图形扩展名
        formData.append('graphInfo', data["graphInfo"]);//图形数据说明
        formData.append('graphComment', data["graphComment"]);//图形备注
        formData.append('isReport', isReport ? 1 : 0);//是否报告
        formData.append('dispOrder', data["dispOrder"]);//显示次序

        //获得原图片宽高
        var img = new Image();
        img.src = imgBase64;
        img.onload = function () {//获得缩略图
            if (img.naturalHeight > me.config.imgHeight || img.naturalWidth > me.config.imgWidth) {
                var width = me.config.imgWidth,
                    height = me.config.imgHeight,
                    Scaling = 1;
                if ((img.naturalHeight - me.config.imgHeight) > (img.naturalWidth - me.config.imgWidth)) {
                    Scaling = img.naturalHeight / me.config.imgHeight;
                } else {
                    Scaling = img.naturalWidth / me.config.imgWidth;
                }
                width = img.naturalWidth / Scaling;
                height = img.naturalHeight / Scaling;
                me.resizeImage(imgBase64, imgType, function (thumbnailBase) {
                    formData.append('graphThumb', thumbnailBase);//缩略图base64
                    formData.append('graphHeight', img.naturalHeight);//图形高度
                    formData.append('graphWidth', img.naturalWidth);//图形宽度
                    me.imgSubmit(formData, loadIndex);
                }, width, height);
            } else {
                formData.append('graphThumb', imgBase64);//缩略图base64
                formData.append('graphHeight', img.naturalHeight);//图形高度
                formData.append('graphWidth', img.naturalWidth);//图形宽度
                me.imgSubmit(formData, loadIndex);
            }
        }
    };
    //图形表单提交
    app.imgSubmit = function (formData, loadIndex) {
        var me = this,
            loadIndex = loadIndex || null,
            url = me.url.addLisTestGraphDataUrl,
            formData = formData || null;
        if (!formData) {
            if (loadIndex) layer.close(loadIndex);
            return;
        }
        //表单提交
        $.ajax({
            url: url,
            type: 'post',
            data: formData,
            cache: false,
            dataType: 'json',
            //contentType: "text",
            processData: false,         // 告诉jQuery不要去处理发送的数据
            contentType: false,        // 告诉jQuery不要去设置Content-Type请求头
            success: function (res) {
                layer.close(loadIndex);
                if (res.success) {
                    if (me.config.addImagelayerIndex) layer.close(me.config.addImagelayerIndex);
                    me.search();
                    uxbase.MSG.onSuccess("保存成功!");
                } else {
                    uxbase.MSG.onError(res.ErrorInfo);
                }
            },
            error: function (e) {
                layer.close(loadIndex);
            }
        });
    };
    //清空图形列表
    app.clearData = function (msg) {
        var me = this,
            msg = msg || '';
        $("#imagesContent").html(msg);
        me.config.imageRightClickInfo.testGraphID = null;
        me.config.imageRightClickInfo.graphDataID = null;
        me.config.imageRightClickInfo.graphDispOrder = null;
        me.config.imageRightClickInfo.graphName = null;
        me.config.imageRightClickInfo.graphElem = null;
    };
    //创建图形
    app.createImage = function (list) {
        var me = this,
            saveCount = list.length,
            saveSuccess = 0,
            saveError = 0,
            img = [],
            list = list || [];
        var load = layer.load();
        $.each(list, function (i, item) {
            var graphSizeType = 0,
                graphWidth = item["LisTestGraph_GraphWidth"],
                graphHeight = item["LisTestGraph_GraphHeight"],
                graphDispOrder = item["LisTestGraph_DispOrder"],
                graphName = item["LisTestGraph_GraphName"],
                graphDataID = item["LisTestGraph_GraphDataID"],
                testGraphID = item["LisTestGraph_Id"];
            setTimeout(function () {
                me.getImageInfo(graphSizeType, graphDataID, function (res) {
                    if (res.success) {
                        saveSuccess++;
                        var imageSrc = res.value;
                        img.push('<img src="' + imageSrc + '" style="width:98%;max-height:100px;border: 1px solid #009688;margin-bottom: 3px;" data-id="' + testGraphID + '" data-graphdataid="' + graphDataID + '" data-width="' + graphWidth + '" data-height="' + graphHeight + '" data-disorder="' + graphDispOrder + '" data-name="' + graphName + '" title="' + graphName + '" />');
                    } else {
                        saveError--;
                    }
                    if (saveSuccess + saveError == saveCount) {
                        $("#imagesContent").html(img.join(''));
                        layer.close(load);
                    }
                });
            }, 100 * i);
        });
    };
    //获得滚动条的高度
    app.getScrollTop = function () {
        var me = this,
            scrollTop = 0;
        if (document.documentElement && document.documentElement.scrollTop) {
            scrollTop = document.documentElement.scrollTop;
        } else if (document.body) {
            scrollTop = document.body.scrollTop;
        }
        return scrollTop;
    }
    //粘贴/paste处理
    app.handlePaste = function (event) {
        var me = this,
            device = layui.device(),
            e = event || window.event;
        if (device.ie) {
            e.returnValue = false;//阻止默认事件失败 -- 使用定时器解决
            setTimeout(function () { $("#pasteImage").html(""); }, 1);
        } else {
            e.preventDefault();
        }
        if (me.config.isReadOnly) return;
        if (!me.config.testFormRecord) {
            uxbase.MSG.onWarn("请选择一条检验单进行粘贴图形结果!");
            return;
        }
        var clipboardData = e.clipboardData || window.clipboardData;//获取图片内容
        var blob;
        if (device.ie) {
            blob = clipboardData.files[0];
            //layer.msg("暂不支持IE浏览器，建议使用谷歌高级浏览器!", { icon: 0, anim: 0 });
        } else {
            blob = clipboardData.items[0].getAsFile();
        }
        if (!blob) return;
        me.blobToDataURL(blob, function (result) {
            var disOrder = 1;
            if ($("#imagesContent img").length > 0) {//已存在img
                disOrder = Number($("#imagesContent img:last").attr("data-disorder")) + 1;
            }
            var imgType = blob.type,
                data = {
                    graphName: "图形" + disOrder,
                    isReport: 1,
                    dispOrder: disOrder
                };
            me.saveImage(result, imgType, data);
        });
        return false;
    };
    //blob转base64
    app.blobToDataURL = function (blob, callback) {
        var a = new FileReader();
        a.onload = function (e) { callback(e.target.result); }
        a.readAsDataURL(blob);
    };
    //根据canvas生成缩略图base64
    app.resizeImage = function (src, type, callback, w, h) {
        var me = this,
            canvas = document.createElement("canvas"),
            ctx = canvas.getContext("2d"),
            type = type || "image/png",
            im = new Image();
        w = w || 0,
            h = h || 0;
        im.onload = function () {
            //为传入缩放尺寸用原尺寸
            !w && (w = this.width);
            !h && (h = this.height);
            //以长宽最大值作为最终生成图片的依据
            if (w !== this.width || h !== this.height) {
                var ratio;
                if (w > h) {
                    ratio = this.width / w;
                    h = this.height / ratio;
                } else if (w === h) {
                    if (this.width > this.height) {
                        ratio = this.width / w;
                        h = this.height / ratio;
                    } else {
                        ratio = this.height / h;
                        w = this.width / ratio;
                    }
                } else {
                    ratio = this.height / h;
                    w = this.width / ratio;
                }
            }
            //以传入的长宽作为最终生成图片的尺寸
            if (w > h) {
                var offset = (w - h) / 2;
                canvas.width = canvas.height = w;
                ctx.drawImage(im, 0, offset, w, h);
            } else if (w < h) {
                var offset = (h - w) / 2;
                canvas.width = canvas.height = h;
                ctx.drawImage(im, offset, 0, w, h);
            } else {
                canvas.width = canvas.height = h;
                ctx.drawImage(im, 0, 0, w, h);
            }
            callback(canvas.toDataURL(type));
        }
        im.src = src;
    };
    //暴露接口
    exports('images', app);
});
