var Lodop = {
    Win32_Install_URL: Shell.util.Path.getRootPath()+'/ui/util/print/resources/CLodop_4_113_32.exe',
    Win32_Update_URL: Shell.util.Path.getRootPath()+'/ui/util/print/resources/CLodop_4_113_32.exe',
    Win64_Install_URL: Shell.util.Path.getRootPath()+'/ui/util/print/resources/CLodop_4_113_32.exe',
    Win64_Update_URL: Shell.util.Path.getRootPath()+'/ui/util/print/resources/CLodop_4_113_32.exe',
    //获取LODOP
    getLodop: function () {
        var me = this,
            lodop = null;
        
        try {
            if (typeof (CLODOP) == "undefined") {
                me.winPromptInstall();
                return;
            } else {
                lodop = getCLodop();
                if (!lodop) return;

                //lodop.SET_LICENSES("北京智方科技开发有限公司", "653726269717472919278901905623", "", "");
                lodop.SET_LICENSES("智方（北京）科技发展有限公司", "F8891E7BACFF708720795EB2B31ACDEED20", "智方（北京）科技發展有限公司", "B42575199843028F59C74AEBE781ED2DA61");
                lodop.SET_LICENSES("THIRD LICENSE", "", "Zhifang (Beijing) Technology Development Co., Ltd", "A5427B2406A3E024721029FE6FD4BE99BC7");
                return lodop;
            }
        } catch (e) {
            return;
        }
    },
    getLodopObj: function (taskName) {
        var me = this,
            lodop = null;

        try {
            if (typeof (CLODOP) == "undefined") {
                me.winPromptInstall();
                return;
            } else {
                lodop = getCLodop();
                if (!lodop) return;

                lodop.PRINT_INIT(taskName);
                //lodop.SET_LICENSES("北京智方科技开发有限公司", "653726269717472919278901905623", "", "");
                lodop.SET_LICENSES("智方（北京）科技发展有限公司", "F8891E7BACFF708720795EB2B31ACDEED20", "智方（北京）科技發展有限公司", "B42575199843028F59C74AEBE781ED2DA61");
                lodop.SET_LICENSES("THIRD LICENSE", "", "Zhifang (Beijing) Technology Development Co., Ltd", "A5427B2406A3E024721029FE6FD4BE99BC7");
                return lodop;
            }
        } catch (e) {
            return;
        }
    },
    //客户端未安装CLODOP时在浏览器进行弹出窗体提示安装
    winPromptInstall: function () {
        var me = this,
            html = [];

        try {
            if ((typeof (CLODOP) == "undefined") || (typeof (LODOP.VERSION) == "undefined")) {
                if (navigator.userAgent.indexOf('Firefox') >= 0) {
                    html.push(me.getStrHtmFireFox());
                }
                if (navigator.userAgent.indexOf('Win64') >= 0) {
                    html.push(me.getStrHtm64_Install());
                } else {
                    html.push(me.getStrHtmInstall());
                }
            } else if (LODOP.VERSION < "6.1.2.0") {
                if (navigator.userAgent.indexOf('Win64') >= 0) {
                    html.push(me.getStrHtm64_Update());
                } else {
                    html.push(me.getStrHtmUpdate());
                }
            }
        } catch (err) {
            if (navigator.userAgent.indexOf('Win64') >= 0) {
                html.push("Error:" + me.getStrHtm64_Install());
            } else {
                html.push("Error:" + me.getStrHtmInstall());
            }
        }

        if (html.length > 0) {
            html = '<div style="padding:25px 15px;font-weight:bold;font-size:14px;">' + html.join('<br>') + '</div>';
            //JShell.Win.open('Ext.panel.Panel', {
            //    title: 'CLODOP打印控件安装提示',
            //    resizable: true,
            //    width: 340,
            //    height: 180,
            //    html: html,
            //    listeners: {
            //        close: function (p, eOpts) { }
            //    }
            //}).show();
            // layer.open({
            //     type: 1,
            //     title: ['CLODOP打印控件安装提示', 'font-weight:bold;'],
            //     skin: 'layui-layer-molv',
            //     anim: 6,
            //     content: html
            // });
            $.messager.alert('警告:CLODOP打印控件安装提示',html,"warning"); 
        }
    },
    /**@description 32位安装包下载提示*/
    getStrHtmInstall: function () {
        var me = this;
        var html = "<font color='#FF00FF'>打印控件未安装!点击这里<a href='" + me.Win32_Install_URL + "'>执行安装</a>,安装后请退出系统重新登录。</font>";
        return html;
    },
    /**@description 32位安装包升级提示*/
    getStrHtmUpdate: function () {
        var me = this;
        var html = "<font color='#FF00FF'>打印控件需要升级!点击这里<a href='" + me.Win32_Update_URL + "'>执行升级</a>,升级后请退出系统重新登录。</font>";
        return html;
    },
    /**@description 64位安装包安装提示*/
    getStrHtm64_Install: function () {
        var me = this;
        var html = "<font color='#FF00FF'>打印控件未安装!点击这里<a href='" + me.Win64_Install_URL + "'>执行安装</a>,安装后请退出系统重新登录。</font>";
        return html;
    },
    /**@description 64位安装包升级提示*/
    getStrHtm64_Update: function () {
        var me = this;
        var html = "<font color='#FF00FF'>打印控件需要升级!点击这里<a href='" + me.Win64_Update_URL + "'>执行升级</a>,升级后请退出系统重新登录。</font>";
        return html;
    },
    /**@description 火狐浏览器的额外提示*/
    getStrHtmFireFox: function () {
        var html = "<font color='#FF00FF'>注意：<br>1：如曾安装过Lodop旧版附件npActiveXPLugin,请在【工具】->【附加组件】->【扩展】中先卸它。</font>";
        return html;
    },
    //模板1(48mm*32mm)
	_Model_1: {
        //类型名称
        Name: '128A(50mm*30mm)',
        //类型编号
        Type: '128A',
        //标题
        Title: 'LODOP.PRINT_INITA(0,0,"50mm","30mm","打印条码");' +
            'LODOP.SET_PRINT_PAGESIZE(0,"50mm","30mm","");',
        //内容体
        Content: 'LODOP.NEWPAGE();' +
            //一维码条码
            'LODOP.ADD_PRINT_BARCODE("2mm","2mm","40mm","10mm","128A","{BARCODE}");' +
            'LODOP.SET_PRINT_STYLEA(0,"QRCodeVersion",{QRCodeVersion});' +
            'LODOP.SET_PRINT_STYLEA(0,"QRCodeErrorLevel","{QRCodeErrorLevel}");' +
            'LODOP.SET_PRINT_STYLEA(0,"DataCharset","UTF-8");'
    },

    //直接打印
    print: function (data, oIndexOrName) {
        var me = this;
        var strLodop = me.getStrLodop(data);
        if (oIndexOrName > -2) {
            strLodop += 'LODOP.SET_PRINTER_INDEX(' + oIndexOrName + ');LODOP.PRINT();';
        } else {
            strLodop += 'LODOP.PRINT();';
        }
        eval(strLodop);
    },
    //预览打印
    preview: function (data, oIndexOrName) {
        var me = this;
        var strLodop = me.getStrLodop(data);
        if (oIndexOrName > -2) {
            strLodop += 'LODOP.SET_PRINTER_INDEX(' + oIndexOrName + ');LODOP.PREVIEW();';
        } else {
            strLodop += 'LODOP.PREVIEW();';
        }
        
        eval(strLodop);
    },
    //获取Lodop字符串
    getStrLodop: function (data, type) {
        var me = this;

        var LODOP = me.getLodop();
        if (!LODOP) return;

        type = type || '1';

        var title = me['_Model_' + type].Title;
        var content = me.getModelContent(data, type);

        var strLodop = title + content;
        return strLodop;
    },
    //获取打印内容
    getModelContent: function (data, type) {
        var me = this;
        type = type || '1';
        var barcode = me['_Model_' + type].Content;

        for (var i in data) {
            var key = i.toLocaleUpperCase();
            if (key == 'BARCODE') {
                var version = me.getQRCodeVersion(data[i]);
                //一维码版本
                barcode = barcode.replace(/\{QRCodeVersion\}/g, version.QRCodeVersion);
                //一维码容错级别
                barcode = barcode.replace(/\{QRCodeErrorLevel\}/g, version.QRCodeErrorLevel);
            }
            var reg = new RegExp('\\{' + key + '\\}', 'g');
            barcode = barcode.replace(reg, data[i]);
        }

        return barcode;
    },
    //获取一维码版本
    getQRCodeVersion: function (value) {
        var result = {
            QRCodeVersion: 5
        };//版本5

        var len = value.replace(/[^\x00-\xff]/g, 'xxx').length;

        if (len <= 84) {//容错M
            result.QRCodeErrorLevel = "M";
        } else {//容错L
            result.QRCodeErrorLevel = "L";
        }

        return result;
    }
}