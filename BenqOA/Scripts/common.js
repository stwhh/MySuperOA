var pagesize = 30;//默认页数量
var alert_selectone = '请至少选择一项！';
var alert_confirmdelete = '确认删除所选项吗？';
var alert_confirmsave = '确认保存吗？';
var alert_confirmcopy = '确认复制所选项吗？';
var alert_confirpublic = '确认发布所选项吗？';
var alert_confirinvalid = '确认作废所选项吗？';
var alert_null = '没有查询到记录';

//Grid新增行
function Grid_AddRow(gridid) {
    $('#' + gridid).data("kendoGrid").addRow();
}

//Grid初始化含有CheckBox(无分页)
function Grid_BindCheckBoxNoPage(gridid, url, columns, querymodel) {
    //居中
    if ($(columns[0].template).length > 0) {
        columns[0]['attributes'] = {
            style: "text-align: center;"
        }
    }
    $('#' + gridid).kendoGrid({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    pagesize = readobj.data.pageSize;
                    var tmpquerymodel = $.extend(true, {}, querymodel);
                    View_ModelBind(tmpquerymodel);
                    $.ajax({
                        url: url,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify(tmpquerymodel),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            //是否是DataTable
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                readobj.success([]);

                            } else {
                                if (typeof (successobj.Data) == "string") {
                                    successobj.Data = JSON.parse(successobj.Data);
                                }
                                if (successobj.Data == null) {
                                    successobj.Data = [];
                                }
                                readobj.success(successobj);
                            }
                            //删除成功
                            //if (obj.TotalCount == 0) {
                            //    alert(alert_null);
                            //}
                        }
                    })
                }
            },
            //图表
            schema: {
                type: 'json',//返回的类型
                data: 'Data',//返回的数据源
                total: 'TotalCount'//返回的总数据量
            },
            serverPaging: true,//服务器分页
            page: 1,//起始索引
            pageSize: pagesize//起始页容量
        }),
        sortable: true,
        autoBind: false,
        scrollable: true,
        resizable: true,
        selectable: true,
        columnMenu: {
            messages: {
                columns: "列"
            }
        },
        columns: columns
        //toolbar: kendo.template($(template).html())
    });

    //全选
    var grid = $('#' + gridid).data("kendoGrid");

    //居中头
    grid.thead.find("th>.selectAll").parent().css('text-align', 'center');

    grid.thead.find("th>.selectAll").on("click", function () {
        var checked = $(this).is(":checked");
        //grid.table.find("tr>td:first>input[type='checkbox'],tr>td:first>div>input[type='checkbox']").prop("checked", checked).trigger("change");
        grid.table.find("tr").find("td:first input[type='checkbox']").prop("checked", checked).trigger("change");

        var checkboxs = grid.table.find("tr").find("td:first input[type='checkbox']");

        //alert(checkboxs.length);

        //grid.table.find("tr").find("td:first>div>input[type='checkbox']").prop("checked", checked).trigger("change");

        //>td:first>input[type='checkbox'],tr>td:first>div>input[type='checkbox']").prop("checked", checked).trigger("change");
    });
    grid.table.find("tr").find("td:first input[type='checkbox']")
        .change(function (e) {
            debugger;
            var checkbox = $(this);
            var selected = grid.table.find("tr").find("td:first input:checked").closest("tr");

            grid.clearSelection();

            if (selected.length) {
                grid.select(selected);
            }

        })
        .end()
        .mousedown(function (e) {
            e.stopPropagation();
        });

    //修改样式
    $('div.k-grid-toolbar').css('padding-top', '1px').css('padding-bottom', '1px');
    $('div.k-pager-wrap').css('padding-top', '1px').css('padding-bottom', '1px');

    //查询
    grid.dataSource.query({
        page: 1, pageSize: pagesize
    });
}

//Grid初始化含有CheckBox(无分页) 带有DataBound事件
function Grid_BindCheckBoxNoPage_KendoUI(gridid, url, columns, querymodel, dataBound) {
    //居中
    if ($(columns[0].template).length > 0) {
        columns[0]['attributes'] = {
            style: "text-align: center;"
        }
    }
    $('#' + gridid).kendoGrid({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    pagesize = readobj.data.pageSize;
                    var tmpquerymodel = $.extend(true, {}, querymodel);
                    View_ModelBind(tmpquerymodel);
                    $.ajax({
                        url: url,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify(tmpquerymodel),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            //是否是DataTable
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                readobj.success([]);

                            } else {
                                if (typeof (successobj.Data) == "string") {
                                    successobj.Data = JSON.parse(successobj.Data);
                                }
                                if (successobj.Data == null) {
                                    successobj.Data = [];
                                }
                                readobj.success(successobj);
                            }
                            //删除成功
                            //if (obj.TotalCount == 0) {
                            //    alert(alert_null);
                            //}
                        }
                    })
                }
            },
            //图表
            schema: {
                type: 'json',//返回的类型
                data: 'Data',//返回的数据源
                total: 'TotalCount'//返回的总数据量
            },
            serverPaging: true,//服务器分页
            page: 1,//起始索引
            pageSize: pagesize//起始页容量
        }),
        sortable: true,
        autoBind: false,
        scrollable: true,
        resizable: true,
        selectable: true,
        dataBound: dataBound,
        columnMenu: {
            messages: {
                columns: "列"
            }
        },
        columns: columns
        //toolbar: kendo.template($(template).html())
    });

    //全选
    var grid = $('#' + gridid).data("kendoGrid");

    //居中头
    grid.thead.find("th>.selectAll").parent().css('text-align', 'center');

    grid.thead.find("th>.selectAll").on("click", function () {
        var checked = $(this).is(":checked");
        //grid.table.find("tr>td>input[type='checkbox'],tr>td>div>input[type='checkbox']").prop("checked", checked).trigger("change");
        //控制网格中存在多个checkbox by neil
        grid.table.find("tr").find("td:first input[type='checkbox']").prop("checked", checked).trigger("change");
    });
    grid.table.find("tr").find("td:first input[type='checkbox']")
        .change(function (e) {
            debugger;
            var checkbox = $(this);
            var selected = grid.table.find("tr").find("td:first input:checked").closest("tr");

            grid.clearSelection();

            if (selected.length) {
                grid.select(selected);
            }

        })
        .end()
        .mousedown(function (e) {
            e.stopPropagation();
        });

    //修改样式
    $('div.k-grid-toolbar').css('padding-top', '1px').css('padding-bottom', '1px');
    $('div.k-pager-wrap').css('padding-top', '1px').css('padding-bottom', '1px');

    //查询
    grid.dataSource.query({
        page: 1, pageSize: pagesize
    });
}

//Grid初始化含有CheckBox
function Grid_BindCheckBox(gridid, url, columns, querymodel) {
    //居中
    if ($(columns[0].template).length > 0) {
        columns[0]['attributes'] = {
            style: "text-align: center;"
        }
    }
    $('#' + gridid).kendoGrid({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    pagesize = readobj.data.pageSize;
                    var tmpquerymodel = $.extend(true, {}, querymodel);
                    View_ModelBind(tmpquerymodel);
                    //附加翻页
                    tmpquerymodel['pageindex'] = readobj.data.page - 1; //自定义pageindex，pagesize两个参数传到后台，后台接收，总页数-1，页数从0开始
                    tmpquerymodel['pagesize'] = pagesize;
                    $.ajax({
                        url: url,
                        contentType: 'application/json', //(默认: "application/x-www-form-urlencoded") 发送信息至服务器时内容编码类型
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify(tmpquerymodel), //tmpquerymodel是传向后台的数据,传pageindex，pagesize(自定义的值)，querymodel
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            //是否是DataTable
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                readobj.success([]);

                            } else {
                                if (typeof (successobj.Data) == "string") {
                                    successobj.Data = JSON.parse(successobj.Data);
                                }
                                if (successobj.Data == null) {
                                    successobj.Data = [];
                                }
                                readobj.success(successobj);
                            }
                        }
                    })
                }
            },
            //图表
            schema: {
                type: 'json',//返回的类型
                data: 'Data',//返回的数据源
                total: 'TotalCount'//返回的总数据量
            },
            serverPaging: true,//服务器分页
            page: 1,//起始索引
            pageSize: pagesize//起始页容量
        }),
        sortable: true,
        autoBind: false,
        scrollable: true,
        resizable: true,
        selectable: true,
        columnMenu: {
            messages: {
                columns: "列"
            }
        },
        columns: columns,
        pageable: {
            pageSize: pagesize,
            pageSizes: [30, 50, 80],
            refresh: true,
            messages: {
                display: "第{0} - {1}项 共 {2} 项", //{0} is the index of the first record on the page, {1} - index of the last record on the page, {2} is the total amount of records
                empty: "未找到记录",
                page: "页",
                of: "共 {0}", //{0} is total amount of pages
                itemsPerPage: "项/页",
                first: "回到首页",
                previous: "上一页",
                next: "下一页",
                last: "最后一页",
                refresh: "刷新"
            }
        }
    });

    //全选
    var grid = $('#' + gridid).data("kendoGrid");

    //居中头
    grid.thead.find("th>.selectAll").parent().css('text-align', 'center');

    grid.thead.find("th>.selectAll").on("click", function () {
        var checked = $(this).is(":checked");
        //grid.table.find("tr>td>input[type='checkbox'],tr>td>div>input[type='checkbox']").prop("checked", checked).trigger("change");
        //控制网格中存在多个checkbox by neil
        grid.table.find("tr").find("td:first input[type='checkbox']").prop("checked", checked).trigger("change");
    });
    grid.table.find("tr").find("td:first input[type='checkbox']")
        .change(function (e) {
            debugger;
            var checkbox = $(this);
            var selected = grid.table.find("tr").find("td:first input:checked").closest("tr");

            grid.clearSelection();

            if (selected.length) {
                grid.select(selected);
            }

        })
        .end()
        .mousedown(function (e) {
            e.stopPropagation();
        });

    //修改样式
    $('div.k-grid-toolbar').css('padding-top', '1px').css('padding-bottom', '1px');
    $('div.k-pager-wrap').css('padding-top', '1px').css('padding-bottom', '1px');

    //查询
    grid.dataSource.query({
        page: 1, pageSize: pagesize
    });
}

//Grid初始化含有CheckBox(编辑)
function Grid_BindCheckBoxEdit(gridid, url, columns, querymodel) {
    //居中
    if ($(columns[0].template).length > 0) {
        columns[0]['attributes'] = {
            style: "text-align: center;"
        }
    }
    $('#' + gridid).kendoGrid({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    pagesize = readobj.data.pageSize;
                    var tmpquerymodel = $.extend(true, {}, querymodel);
                    View_ModelBind(tmpquerymodel);
                    //附加翻页
                    tmpquerymodel['pageindex'] = readobj.data.page - 1;
                    tmpquerymodel['pagesize'] = pagesize;
                    $.ajax({
                        url: url,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify(tmpquerymodel),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            //是否是DataTable
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                readobj.success([]);

                            } else {
                                if (typeof (successobj.Data) == "string") {
                                    successobj.Data = JSON.parse(successobj.Data);
                                }
                                if (successobj.Data == null) {
                                    successobj.Data = [];
                                }
                                readobj.success(successobj);
                            }
                            //删除成功
                            //if (obj.TotalCount == 0) {
                            //    alert(alert_null);
                            //}
                        }
                    })
                }
            },

            //图表
            schema: {
                type: 'json',//返回的类型
                data: 'Data',//返回的数据源
                total: 'TotalCount',//返回的总数据量
                //  model:schemaModel
            },
            serverPaging: true,//服务器分页
            page: 1,//起始索引
            pageSize: pagesize//起始页容量
        }),
        sortable: true,
        autoBind: false,
        scrollable: true,
        resizable: true,
        selectable: true,
        columnMenu: {
            messages: {
                columns: "列"
            }
        },
        columns: columns,
        pageable: {
            pageSize: pagesize,
            pageSizes: [30, 50, 80],
            refresh: true,
            messages: {
                display: "第{0} - {1}项 共 {2} 项", //{0} is the index of the first record on the page, {1} - index of the last record on the page, {2} is the total amount of records
                empty: "未找到记录",
                page: "页",
                of: "共 {0}", //{0} is total amount of pages
                itemsPerPage: "项/页",
                first: "回到首页",
                previous: "上一页",
                next: "下一页",
                last: "最后一页",
                refresh: "刷新"
            }
        },
        editable: true,
        edit: function (e) {
            e.container.parent().find('td:first input').attr('edited', 'true');
        }
        //change: function (e) {
        //    var selectedRows = this.select();
        //    var firstcheckbox = selectedRows.find('input[type="checkbox"]').first();
        //    if (View_IsNullOrEmpty(firstcheckbox.attr('checked'))) {
        //        firstcheckbox.attr('checked', 'checked');
        //    }
        //    else {
        //        firstcheckbox.removeAttr('checked');
        //    }
        //}

    });

    //全选
    var grid = $('#' + gridid).data("kendoGrid");

    //居中头
    grid.thead.find("th>.selectAll").parent().css('text-align', 'center');

    grid.thead.find("th>.selectAll").on("click", function () {
        var checked = $(this).is(":checked");
        //grid.table.find("tr>td>input[type='checkbox'],tr>td>div>input[type='checkbox']").prop("checked", checked).trigger("change");
        //控制网格中存在多个checkbox by neil
        grid.table.find("tr").find("td:first input[type='checkbox']").prop("checked", checked).trigger("change");
    });
    grid.table.find("tr").find("td:first input[type='checkbox']")
        .change(function (e) {
            debugger;
            var checkbox = $(this);
            var selected = grid.table.find("tr").find("td:first input:checked").closest("tr");

            grid.clearSelection();

            if (selected.length) {
                grid.select(selected);
            }

        })
        .end()
        .mousedown(function (e) {
            e.stopPropagation();
        });

    //修改样式
    $('div.k-grid-toolbar').css('padding-top', '1px').css('padding-bottom', '1px');
    $('div.k-pager-wrap').css('padding-top', '1px').css('padding-bottom', '1px');

    //查询
    grid.dataSource.query({
        page: 1, pageSize: pagesize
    });
}

//Grid初始化含有CheckBox  向Grid(表格)中添加Kendo UI控件
function Grid_BindCheckBox_KendoUI(gridid, url, columns, querymodel, dataBound) {
    //居中
    if ($(columns[0].template).length > 0) {
        columns[0]['attributes'] = {
            style: "text-align: center;"
        }
    }
    $('#' + gridid).kendoGrid({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    pagesize = readobj.data.pageSize;
                    var tmpquerymodel = $.extend(true, {}, querymodel);
                    View_ModelBind(tmpquerymodel);
                    //附加翻页
                    tmpquerymodel['pageindex'] = readobj.data.page - 1;
                    tmpquerymodel['pagesize'] = pagesize;
                    $.ajax({
                        url: url,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify(tmpquerymodel),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            //是否是DataTable
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                readobj.success([]);

                            } else {
                                if (typeof (successobj.Data) == "string") {
                                    successobj.Data = JSON.parse(successobj.Data);
                                }
                                if (successobj.Data == null) {
                                    successobj.Data = [];
                                }
                                readobj.success(successobj);
                            }
                            //删除成功
                            //if (obj.TotalCount == 0) {
                            //    alert(alert_null);
                            //}
                        }
                    })
                }
            },
            //图表
            schema: {
                type: 'json',//返回的类型
                data: 'Data',//返回的数据源
                total: 'TotalCount'//返回的总数据量
            },
            serverPaging: true,//服务器分页
            page: 1,//起始索引
            pageSize: pagesize//起始页容量
        }),
        //editable: true,
        sortable: true,
        autoBind: false,
        scrollable: true,
        resizable: true,
        selectable: true,
        dataBound: dataBound,
        columnMenu: {
            messages: {
                columns: "列"
            }
        },
        columns: columns,
        pageable: {
            pageSize: pagesize,
            pageSizes: [30, 50, 80],
            refresh: true,
            messages: {
                display: "第{0} - {1}项 共 {2} 项", //{0} is the index of the first record on the page, {1} - index of the last record on the page, {2} is the total amount of records
                empty: "未找到记录",
                page: "页",
                of: "共 {0}", //{0} is total amount of pages
                itemsPerPage: "项/页",
                first: "回到首页",
                previous: "上一页",
                next: "下一页",
                last: "最后一页",
                refresh: "刷新"
            }
        },
        dataBound: dataBound

        //toolbar: kendo.template($(template).html())

    });

    //全选
    var grid = $('#' + gridid).data("kendoGrid");

    //居中头
    grid.thead.find("th>.selectAll").parent().css('text-align', 'center');

    grid.thead.find("th>.selectAll").on("click", function () {
        var checked = $(this).is(":checked");
        //grid.table.find("tr>td>input[type='checkbox'],tr>td>div>input[type='checkbox']").prop("checked", checked).trigger("change");
        //控制网格中存在多个checkbox by neil
        grid.table.find("tr").find("td:first input[type='checkbox']").prop("checked", checked).trigger("change");
    });
    grid.table.find("tr").find("td:first input[type='checkbox']")
        .change(function (e) {
            debugger;
            var checkbox = $(this);
            var selected = grid.table.find("tr").find("td:first input:checked").closest("tr");

            grid.clearSelection();

            if (selected.length) {
                grid.select(selected);
            }

        })
        .end()
        .mousedown(function (e) {
            e.stopPropagation();
        });

    //修改样式
    $('div.k-grid-toolbar').css('padding-top', '1px').css('padding-bottom', '1px');
    $('div.k-pager-wrap').css('padding-top', '1px').css('padding-bottom', '1px');

    //查询
    grid.dataSource.query({
        page: 1, pageSize: pagesize
    });
}

//Grid查询
function Grid_Query(gridid) {
    var girddatasource = $('#' + gridid).data("kendoGrid").dataSource;
    var gridpagesize = girddatasource.pageSize();
    girddatasource.query({
        page: 1, pageSize: gridpagesize
    });
    $('#' + gridid).data('kendoGrid').thead.find("th>.selectAll").removeAttr('checked')
}

//Grid抓取勾选到的Entities
function Grid_GetSelectdEntities(gridid) {
    var selectentities = [];
    var grid = $('#' + gridid).data('kendoGrid');
    var list = grid.table.find('tr').find('td:first input');
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked) {
            var entity = $.grep($(grid.dataSource.data()), function (item) {
                return item.uid == list[i].value;
            })[0];
            selectentities.push(entity);
        }
    }
    return selectentities;
}

//Grid抓取全部的Entities,返回字典数组
function Grid_GetAllEntities(gridid) {
    var selectentities = [];
    var grid = $('#' + gridid).data('kendoGrid');
    var list = grid.table.find('tr').find('td:first input');
    for (var i = 0; i < list.length; i++) {
        var entity = $.grep($(grid.dataSource.data()), function (item) {
            return item.uid == list[i].value;
        })[0];
        entity['CHECKED'] = list[i].checked;
        selectentities.push(entity);
    }
    return selectentities;
}

//Grid抓取已编辑到的Entities
function Grid_GetEditedEntities(gridid) {
    var selectentities = [];
    var grid = $('#' + gridid).data('kendoGrid');
    var list = grid.table.find('tr').find('td:first input');
    for (var i = 0; i < list.length; i++) {
        if ($(list[i]).attr('edited') == 'true') {
            var entity = $.grep($(grid.dataSource.data()), function (item) {
                return item.uid == list[i].value;
            })[0];
            selectentities.push(entity);
        }
    }
    return selectentities;
}


//Grid执行操作
function Grid_Execution(gridid, url, data, callback) {

    //把JOSN类型的数组传入方法
    $.ajax({
        url: url,
        contentType: 'application/json',
        type: 'post',
        dataType: 'json',
        data: JSON.stringify(data),
        success: function (successobj) {
            if (successobj.ErrorCode == "999") {
                ShowAlert(successobj.Message, GoToLogin);
                return;
            }
            if (successobj.ErrorCode == "0") {
                //正确刷新Grid
                if (callback) {
                    callback(successobj);
                    gridid && Grid_Query(gridid);
                } else {
                    ShowAlert(successobj.Message, function () {
                        gridid && Grid_Query(gridid);
                    });
                }

            }
            else {
                //错误弹出异常信息
                ShowAlert(successobj.Message);
            }
        }
    });
}

//Grid保存
function Gird_Save(gridid, url, callback) {
    var selectentities = Grid_GetSelectdEntities(gridid);
    //如果是空，返回未选择
    if (selectentities.length == 0) {
        ShowAlert(alert_selectone);
        return;
    }
    ShowConfirm(alert_confirmsave, function () {
        Grid_Execution(gridid, url, selectentities, callback);
    });
}

//Grid新增后保存
function Gird_EditedSave(gridid, url, callback) {
    var selectentities = Grid_GetEditedEntities(gridid);
    //如果是空，返回未选择
    if (selectentities.length == 0) {
        ShowAlert(alert_selectone);
        return;
    }
    ShowConfirm(alert_confirmsave, function () {
        Grid_Execution(gridid, url, selectentities, callback);
    });

}

//Grid删除
function Gird_Delete(gridid, url) {
    var selectentities = Grid_GetSelectdEntities(gridid);
    //如果是空，返回未选择
    if (selectentities.length == 0) {
        ShowAlert(alert_selectone);
        return;
    }
    ShowConfirm(alert_confirmdelete, function () {
        Grid_Execution(gridid, url, selectentities);
    });
}

//Grid操作
function Gird_Operation(gridid, url, confirmmes, callback) {
    var selectentities = Grid_GetSelectdEntities(gridid);
    //如果是空，返回未选择
    if (selectentities.length == 0) {
        ShowAlert(alert_selectone);
        return;
    }
    if (View_IsNullOrEmpty(confirmmes)) {
        Grid_Execution(gridid, url, selectentities, callback);
    }
    else {
        ShowConfirm(confirmmes, function () {
            Grid_Execution(gridid, url, selectentities, callback);
        });
    }
}

//视图模板绑定
function View_ModelBind(querymodel) {
    for (var obj in querymodel) {
        try {
            querymodel[obj] = querymodel[obj]();
        }
        catch (ex) {
            querymodel[obj] = querymodel[obj];
        };
    }
}

function QueryModelToPostModel(querymodel) {
    var postmodel = {};
    for (var obj in querymodel) {
        try {
            postmodel[obj] = querymodel[obj]();
        }
        catch (ex) {
            postmodel[obj] = querymodel[obj];
        };
    }
    return postmodel;
}

//带有回掉函数的 View执行操作
function View_Execution_CallBack(url, datamodel, callback) {
    var tmpdatamodel = $.extend(true, {}, datamodel);
    View_ModelBind(tmpdatamodel);
    //把JOSN类型的数组传入方法
    $.ajax({
        url: url,
        contentType: 'application/json',
        type: 'post',
        dataType: 'json',
        data: JSON.stringify(tmpdatamodel),
        success: function (successobj) {
            if (successobj.ErrorCode == "999") {
                ShowAlert(successobj.Message, GoToLogin);
                return;
            }
            if (successobj.ErrorCode == "0") {
                //正确
                if (successobj.Message != "" && successobj.Message != null) {
                    ShowAlert(successobj.Message, function () {
                        if (callback != null) {
                            callback(successobj);
                        }
                    });
                } else {
                    if (callback != null) {
                        callback(successobj);
                    }
                }
            }
            else {
                //错误弹出异常信息
                ShowAlert(successobj.Message, function () {
                    if (callback != null) {
                        callback(successobj);
                    }
                });
            }
        }
    });
}

//带有回掉函数的 View执行操作
function View_ExecutionList_CallBack(url, datamodel, callback) {
    //把JOSN类型的数组传入方法
    $.ajax({
        url: url,
        contentType: 'application/json',
        type: 'post',
        dataType: 'json',
        data: JSON.stringify(datamodel),
        success: function (successobj) {
            if (successobj.ErrorCode == "999") {
                ShowAlert(successobj.Message, GoToLogin);
                return;
            }
            if (successobj.ErrorCode == "0") {
                //正确
                ShowAlert(successobj.Message, function () {
                    if (callback != null) {
                        callback(successobj);
                    }
                });
            }
            else {
                //错误弹出异常信息
                ShowAlert(successobj.Message, function () {
                    if (callback != null) {
                        callback(successobj);
                    }
                });
            }
        }
    });
}

function getMainDoc(pdoc) {
    return pdoc.top;


}

//View弹出窗口 带有回传参数 用于点击关闭窗口按钮 刷新父页面   
function View_ShowWindow_CallBack(setheight, setwidth, settitle, seturl, callback) {
    //获取id
    var windowid = $('#hidwindowid').val();

    var mainDoc = getMainDoc(this);

    var divWind = mainDoc.document.createElement('div');
    divWind.id = windowid;

    var wind = mainDoc.document.body.appendChild(divWind);
    var p_winid = getQueryString('winid');
    if (!p_winid) {
        p_winid = "";
    }
    mainDoc.CreatePOPWindow(windowid, p_winid, setheight, setwidth, settitle, seturl, callback);
}
function CreatePOPWindow(windowid, p_winid, setheight, setwidth, settitle, seturl, callback) {

    //清空内容
    var windowdata = $('#' + windowid).data("kendoWindow");
    if (windowdata != null) {
        $('#' + windowid).replaceWith('<div id=' + windowid + '><div>');
    }

    var window = $('#' + windowid).kendoWindow({
        height: setheight,
        width: setwidth,
        animation: false,//动画
        iframe: true,//内联框架
        visible: false,//可见
        modal: true,//父亲页面不可编辑
        close: function () {
            if (callback != null) {
                callback();
            }
            $('#' + windowid).remove();
        },
        //actions: [
        //     "Pin", //固定
        //     "Minimize", //最小化
        //     "Maximize", //最新大化
        //     "Close" //关闭
        //],
    });


    //分析URL
    if (seturl.indexOf('?') > 0) {
        seturl += "&winid=" + windowid + "&p_winid=" + p_winid;
    } else {
        seturl += "?winid=" + windowid + "&p_winid=" + p_winid;
    }
    //设置title
    window.data('kendoWindow').title(settitle);
    //居中弹出
    window.data('kendoWindow').refresh({
        url: encodeURI(seturl)
    }).center().open();
    //先设置iframe的高宽 99% 避免出现滚动条
    $('iframe', window).css('width', '99%').css('height', '99%');
}
function ClosePOPWindow(windowid) {
    $('#' + windowid).data('kendoWindow').close();
    $('#' + windowid).remove();
}
//View关闭窗口
function View_CloseWindow() {
    //获取id

    var mainDoc = getMainDoc(this);

    var windowid = getQueryString('winid');
    mainDoc.ClosePOPWindow(windowid);
}

//子窗口调用父窗口定义的方法
function getParentWin() {

    var mainDoc = getMainDoc(this);
    var p_winid = getQueryString('p_winid');
    if (p_winid == "" || p_winid == null) {
        return mainDoc;
    } else {
        return mainDoc.$("#" + p_winid + " > iframe")[0].contentWindow;

    }

}


function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}


function View_ShowWindowLoading() {
    var container = $('#loadingwindow');
    if (container.data('kendoWindow') == null) {
        container.kendoWindow({
            minWidth: 300,
            minHeight: 100,
            title: '数据源正在生成,请稍后...',
            actions: ["Close"],
            modal: true
        });
        containerWindow = container.data("kendoWindow");
    }

    containerWindow.center().open();


}

function View_CloseWindowLoading() {
    $('#loadingwindow').data('kendoWindow').close();
}

//DropDownList绑定参数表
function DropDownList_BindParameter(dropdownlistid, url, pcategory, text, value) {
    //var common_select = View_Language('COMMON_SELECT');
    $('#' + dropdownlistid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {

                    $.ajax({
                        url: url,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Pcategory': pcategory }),
                        success: function (successobj) {
                            if (readobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        //optionLabel: common_select,
        //optionLabel:"所有",
        dataTextField: text,
        dataValueField: value
    })
}
//DropDownList绑定
function DropDownList_Bind(dropdownlistid, url, value) {

    var common_select = View_Language('COMMON_SELECT');
    $('#' + dropdownlistid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {

                    $.ajax({
                        url: url,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': value }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作

                        }
                    });
                }
            }
        }),
        optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value"
    })
}

//DropDownList绑定(未登录)
function DropDownList_BindNoLogin(dropdownlistid, url, value) {
    var common_select = "请选择";
    $('#' + dropdownlistid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {

                    $.ajax({
                        url: url,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': value }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作

                        }
                    });
                }
            }
        }),
        optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value"
    })
}

//DropDownList绑定 域区域多语言路径 控制
function DropDownList_Bind_Areas(language, dropdownlistid, url, value) {
    var common_select = language;
    $('#' + dropdownlistid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {

                    $.ajax({
                        url: url,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': value }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作

                        }
                    });
                }
            }
        }),
        optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value"
    })
}

//DropDownList绑定 by ID
function DropDownList_BindNoSelect(dropdownlistid, url, value) {
    $('#' + dropdownlistid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    $.ajax({
                        url: url,
                        //    async: false,//同步
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': value }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        dataTextField: 'Text',
        dataValueField: "Value"
    });
}

//DropDownList绑定 by Name
function DropDownList_BindNoSelectByName(dropdownlist, url, value) {
    $(dropdownlist).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    $.ajax({
                        url: url,
                        //    async: false,//同步
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': value }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        dataTextField: 'Text',
        dataValueField: "Value"
    });
}

//DropDownList绑定
function DropDownList_BindNoSelectAsync(dropdownlistid, url, value) {
    $('#' + dropdownlistid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    $.ajax({
                        url: url,
                        async: false,//同步
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': value }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作


                        }
                    });
                }
            }
        }),
        dataTextField: 'Text',
        dataValueField: "Value"
    });
}

//DropDownList绑定
function DropDownList_BindParameter_dataBound(dropdownlistid, url, pcategory, text, value, dataBound) {
    $('#' + dropdownlistid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {

                    $.ajax({
                        url: url,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Pcategory': pcategory }),
                        success: function (successobj) {
                            if (readobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        dataTextField: text,
        dataValueField: value,
        dataBound: dataBound
    })
}

//DropDownList绑定联动
function DropDownList_BindLinkage(firstid, firsturl, secondid, secondurl) {
    var common_select = View_Language('COMMON_SELECT');
    $('#' + firstid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {

                    $.ajax({
                        url: firsturl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': '' }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value",
        change: function () {
            $('#' + secondid).data("kendoDropDownList").dataSource.read();
        }
    });
    $('#' + secondid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    var firstdropdownlist = $('#' + firstid).data("kendoDropDownList");
                    var firstselectedvalue = firstdropdownlist == null ? '' : firstdropdownlist._selectedValue;
                    $.ajax({
                        url: secondurl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': firstselectedvalue }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            $.each(successobj.Data, function (index, obj) {
                                obj['ParentValue'] = firstselectedvalue;
                            })
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        cascadeFrom: firstid, //firstid
        cascadeFromField: "ParentValue",
        optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value"
    });
}


//DropDownList绑定联动
function DropDownList_BindLinkageNoSelect(firstid, firsturl, secondid, secondurl) {
    $('#' + firstid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {

                    $.ajax({
                        url: firsturl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': '' }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        dataTextField: 'Text',
        dataValueField: "Value",
        change: function () {
            $('#' + secondid).data("kendoDropDownList").dataSource.read();
        }
    });
    $('#' + secondid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    var firstdropdownlist = $('#' + firstid).data("kendoDropDownList");
                    var firstselectedvalue = firstdropdownlist == null ? '' : firstdropdownlist._selectedValue;
                    $.ajax({
                        url: secondurl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': firstselectedvalue }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            $.each(successobj.Data, function (index, obj) {
                                obj['ParentValue'] = firstselectedvalue;
                            })
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        cascadeFrom: firstid, //firstid
        cascadeFromField: "ParentValue",
        dataTextField: 'Text',
        dataValueField: "Value"
    });
}

//Editor 富文本编辑器
function Editor(editorid) {
    $("#" + editorid).kendoEditor({
        //工具栏上的工具
        encoded: false, //存储时不编码,默认是编码的
        tools: [
           "formatting",  //格式化
           "bold",       //加粗
           "italic",     //倾斜
           "underline",  //下划线
           "backColor",   //背景色
           "foreColor",    //字体颜色
           "strikethrough",//中间线
           "superscript",  //上标
           "subscript",    //下标
           "justifyLeft",  //左居中
           "justifyCenter",//居中
           "justifyRight", //有聚众
           "justifyFull",
           "insertUnorderedList",
           "insertOrderedList",
           "indent",
           "outdent",
           "createLink",
           "unlink",
           "insertImage",
           //"insertFile",
           //"insertHtml",
           "fontName", //字体名
           //"fontNameInherit",
           "fontSize", //字体大小
           //"fontSizeInherit",
           "formatBlock",
           "style",
           "viewHtml",
           "emptyFolder",
           "uploadFile",
           "orderBy",
           "orderBySize",
           "orderByName",
           "invalidFileType",
           "deleteFile",
           "overwriteFile",
           "directoryNotFound",
           "imageWebAddress",
           "imageAltText",
           "fileWebAddress",
           "fileTitle",
           "linkWebAddress",
           "linkText",
           "linkToolTip",
           "linkOpenInNewWindow",
           "dialogInsert",
           "dialogUpdate",
           "dialogCancel",
           "dialogCancel",
           "createTable",
           "addColumnLeft",
           "addColumnRight",
           "addRowAbove",
           "addRowBelow",
           "deleteRow",
           "deleteColumn"

        ],

        //鼠标放上去的提示，已经引入了中文，这个就没必要了
        //messages: {   
        //    bold: "Bold",
        //    italic: "Italic",
        //    underline: "Underline",
        //    strikethrough: "Strikethrough",
        //    superscript: "Superscript",
        //    subscript: "Subscript",
        //    justifyCenter: "Center text",
        //    justifyLeft: "Align text left",
        //    justifyRight: "Align text right",
        //    justifyFull: "Justify",
        //    insertUnorderedList: "Insert unordered list",
        //    insertOrderedList: "Insert ordered list",
        //    indent: "Indent",
        //    outdent: "Outdent",
        //    createLink: "Insert hyperlink",
        //    unlink: "Remove hyperlink",
        //    insertImage: "Insert image",
        //    insertFile: "Insert file",
        //    insertHtml: "Insert HTML",
        //    fontName: "Select font family",
        //    fontNameInherit: "(inherited font)",
        //    fontSize: "Select font size",
        //    fontSizeInherit: "(inherited size)",
        //    formatBlock: "Format",
        //    formatting: "Format",
        //    style: "Styles",
        //    viewHtml: "View HTML",
        //    emptyFolder: "Empty Folder",
        //    uploadFile: "Upload",
        //    orderBy: "Arrange by:",
        //    orderBySize: "Size",
        //    orderByName: "Name",
        //    invalidFileType: "The selected file \"{0}\" is not valid. Supported file types are {1}.",
        //    deleteFile: "Are you sure you want to delete \"{0}\"?",
        //    overwriteFile: "A file with name \"{0}\" already exists in the current directory. Do you want to overwrite it?",
        //    directoryNotFound: "A directory with this name was not found.",
        //    imageWebAddress: "Web address",
        //    imageAltText: "Alternate text",
        //    fileWebAddress: "Web address",
        //    fileTitle: "Title",
        //    linkWebAddress: "Web address",
        //    linkText: "Text",
        //    linkToolTip: "ToolTip",
        //    linkOpenInNewWindow: "Open link in new window",
        //    dialogInsert: "Insert",
        //    dialogUpdate: "Update",
        //    dialogCancel: "Cancel",
        //    dialogCancel: "Cancel",
        //    createTable: "Create table",
        //    addColumnLeft: "Add column on the left",
        //    addColumnRight: "Add column on the right",
        //    addRowAbove: "Add row above",
        //    addRowBelow: "Add row below",
        //    deleteRow: "Delete row",
        //    deleteColumn: "Delete column",
        //}

    });

}

//三级联动
function DropDownList_BindLinkageThreeLevel(firstid, firsturl, secondid, secondurl, thirdid, thirdurl) {
    var common_select = View_Language('COMMON_SELECT');
    $('#' + firstid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {

                    $.ajax({
                        url: firsturl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': '' }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value",
        change: function () {
            $('#' + secondid).data("kendoDropDownList").dataSource.read();
        }
    });
    $('#' + secondid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    var firstdropdownlist = $('#' + firstid).data("kendoDropDownList");
                    var firstselectedvalue = firstdropdownlist == null ? '' : firstdropdownlist._selectedValue;
                    $.ajax({
                        url: secondurl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': firstselectedvalue }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            $.each(successobj.Data, function (index, obj) {
                                obj['ParentValue'] = firstselectedvalue;
                            })
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        cascadeFrom: firstid, //firstid
        cascadeFromField: "ParentValue",
        optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value",
        change: function () {
            $('#' + thirdid).data("kendoDropDownList").dataSource.read();
        }
    });
    $('#' + thirdid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    var seconddropdownlist = $('#' + secondid).data("kendoDropDownList");
                    var secondselectedvalue = seconddropdownlist == null ? '' : seconddropdownlist._selectedValue;
                    $.ajax({
                        url: thirdurl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': secondselectedvalue }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            $.each(successobj.Data, function (index, obj) {
                                obj['SecondParentValue'] = secondselectedvalue;
                            })
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        cascadeFrom: secondid, //firstid
        cascadeFromField: "SecondParentValue",
        optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value"
    });
}

//三级联动(未登录)
function DropDownList_BindLinkageThreeLevelNoLogin(firstid, firsturl, secondid, secondurl, thirdid, thirdurl) {
    //var common_select = "请选择";
    $('#' + firstid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {

                    $.ajax({
                        url: firsturl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': '' }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        //optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value",
        change: function () {
            $('#' + secondid).data("kendoDropDownList").dataSource.read();
        }
    });
    $('#' + secondid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    var firstdropdownlist = $('#' + firstid).data("kendoDropDownList");
                    var firstselectedvalue = firstdropdownlist == null ? '' : firstdropdownlist._selectedValue;
                    $.ajax({
                        url: secondurl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': firstselectedvalue }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            $.each(successobj.Data, function (index, obj) {
                                obj['ParentValue'] = firstselectedvalue;
                            })
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        cascadeFrom: firstid, //firstid
        cascadeFromField: "ParentValue",
        //optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value",
        change: function () {
            $('#' + thirdid).data("kendoDropDownList").dataSource.read();
        }
    });
    $('#' + thirdid).kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: function (readobj) {
                    var seconddropdownlist = $('#' + secondid).data("kendoDropDownList");
                    var secondselectedvalue = seconddropdownlist == null ? '' : seconddropdownlist._selectedValue;
                    $.ajax({
                        url: thirdurl,
                        contentType: 'application/json',
                        type: 'post',
                        dataType: 'json',
                        data: JSON.stringify({ 'Value': secondselectedvalue }),
                        success: function (successobj) {
                            if (successobj.ErrorCode == "999") {
                                ShowAlert(successobj.Message, GoToLogin);
                                return;
                            }
                            if (successobj.ErrorCode != "0") {
                                ShowAlert(successobj.Message);
                                return;
                            }
                            $.each(successobj.Data, function (index, obj) {
                                obj['SecondParentValue'] = secondselectedvalue;
                            })
                            readobj.success(successobj.Data);//绑定动作
                        }
                    });
                }
            }
        }),
        cascadeFrom: secondid, //firstid
        cascadeFromField: "SecondParentValue",
        //optionLabel: common_select,
        dataTextField: 'Text',
        dataValueField: "Value"
    });
}

//日期绑定
function DatePicker_Bind(datepickerid) {
    $('#' + datepickerid).kendoDatePicker({
        start: 'year',
        depth: 'month',
        culture: 'zh-CN',
        format: "yyyy-MM-dd", //此出修改格式后可能过好长时间系统才能修改过来，最好关闭项目重新打开
        //parseFormats: "yyyy-MM-dd", //无需这个
        max: new Date(2050, 5, 1) //最大值是当前日期  new Date(2013, 2, 1), 最大值2013-2-1
    });
    $('#' + datepickerid).attr("readonly", "readonly");
}

//时间绑定
function DateTimePicker_Bind(datepickerid, inputFormat) {
    $('#' + datepickerid).kendoDateTimePicker({
        format: inputFormat,
        value: new Date()
    });
    $('#' + datepickerid).attr("readonly", "readonly");
}


//判断空值
function View_IsNullOrEmpty(str) {
    if ($.trim(str) == '' || str == null) {
        return true;
    }
    else {
        return false;
    }
}

//校验数据
var checkdata = function (json) {
    if (json instanceof Array) {
        for (var ele in json) {
            if (!checkdata(json[ele])) {
                return false;
            }
        }
        return true;
    }

    var result = true;
    switch (json.type) {
        case "not empty":/*checkdata({ id: "id1", name: "txt1", type: "not empty" })*/
            var $obj = json.id ? $("#" + json.id) : json.$element;
            if ($obj.length > 0 && $obj.val().length < 1) {
                ShowAlert("请输入--" + json.name);
                return false;
            } else {
                return true;
            };
            break;
        case "same":/* checkdata({ id: ["id1", "id2", "id3"], name: ["txt1", "txt2","txt3"], type: "same" })*/
            if (json.id.length > 1 && json.name.length > 1) {
                var first = $("#" + json.id[0]).val();
                $.each(json.id, function (i, n) {
                    if (i >= 1 && $("#" + json.id[i]).val() != first) {
                        ShowAlert("请检查--[" + json.name + "]的数据必须相同");
                        return false;
                    }
                });
            }
            break;
        case "not same":/* checkdata({ id: ["OldPassword", "NewPassword"], name: ["旧密码", "新密码"], type: "not same" })*/
            if (json.id.length > 1 && json.name.length > 1) {
                var arr = [];
                for (var i = 0; i < json.id.length ; i++) {
                    var val = $("#" + json.id[i]).val();
                    if (arr.contains(val)) {
                        ShowAlert("请检查--[" + json.name + "]的数据必须不同");
                        return false;
                    } else {
                        arr.push(val);
                    }
                }
            }
            break;
        case "empty": break;
    }
    return result;
}

//只能输入整数
$.fn.numeralinput = function () {
    $(this).css("ime-mode", "disabled");
    this.bind("keypress", function (e) {
        if (e.charCode === 0) return true;  //非字符键 for firefox
        var code = (e.keyCode ? e.keyCode : e.which);  //兼容火狐 IE    
        return code >= 48 && code <= 57;
    });
    this.bind("blur", function () {
        if (!/\d+/.test(this.value)) {
            this.value = "";
        }
    });
    this.bind("paste", function () {
        if (window.clipboardData) {
            var s = clipboardData.getData('text');
            if (!/\D/.test(s)) {
                value = parseInt(s, 10);
                return true;
            }
        }
        return false;
    });
    this.bind("dragenter", function () {
        return false;
    });
    this.bind("keyup", function () {
        if (this.value !== '0' && /(^0+)/.test(this.value)) {
            this.value = parseInt(this.value, 10);
        }
    });
    this.bind("propertychange", function (e) {
        if (isNaN(this.value))
            this.value = this.value.replace(/\D/g, "");
    });
    this.bind("input", function (e) {
        if (isNaN(this.value))
            this.value = this.value.replace(/\D/g, "");
    });
};

(function ($) {
    //只能输入实数
    $.fn.floatinput = function () {
        var that = this;
        $(this).css("ime-mode", "disabled");
        this.bind("keypress", function (e) {
            if (e.charCode === 0) return true;  //非字符键 for firefox
            var code = (e.keyCode ? e.keyCode : e.which);  //兼容火狐 IE  
            var hasdot = $(this).val() && $(this).val().indexOf(".") > -1;
            var fushu = $(this).val() && (this.value.substr(0, 1) == "-");

            return (code >= 48 && code <= 57) || (!hasdot && code == 46) || (!fushu && code == 45);
        });
        this.bind("blur", function () {
            if (!/\d+.?(\d+)?/.test(this.value)) {
                this.value = "";
            }
        });
        this.bind("paste", function () {
            if (window.clipboardData) {
                var s = clipboardData.getData('text');
                if (!/\D/.test(s)) {
                    value = parseInt(s, 10);
                    return true;
                }
            }
            return false;
        });
        this.bind("dragenter", function () {
            return false;
        });
        this.bind("keyup", function (e) {
            var fushu = $(this).val() && (this.value.substr(0, 1) == "-");
            if (this.value.length == 1 && fushu) {
            }
            else
                if (this.value.indexOf('.') == (this.value.length - 1)) {
                }
                else if (this.value !== '0' && /(^0+)/.test(this.value)) {
                    this.value = parseFloat(this.value, 10);
                }
        });
        this.bind("propertychange", function (e) {
            var fushu = $(this).val() && (this.value.substr(0, 1) == "-");
            if (this.value.length == 1 && fushu) {
            }
            else if (this.value.indexOf('.') == (this.value.length - 1)) {
            }
            else if (isNaN(this.value)) {
                var dotindex = $(this).val().indexOf(".");
                this.value = this.value.replace(/\D/g, "");
                if (dotindex > -1) {
                    this.value = this.value.substr(0, dotindex) + "." + this.value.substr(dotindex);
                }
                if (fushu) {
                    this.value = "-" + this.value;
                }
            }
        });
        this.bind("input", function (e) {
            var fushu = $(this).val() && (this.value.substr(0, 1) == "-");
            if (this.value.length == 1 && fushu) {
            }
            else if (this.value.indexOf('.') == (this.value.length - 1)) {
            }
            else if (isNaN(this.value)) {
                var dotindex = $(this).val().indexOf(".");
                this.value = this.value.replace(/\D/g, "");
                if (dotindex > -1) {
                    this.value = this.value.substr(0, dotindex) + "." + this.value.substr(dotindex);
                }
                if (fushu) {
                    this.value = "-" + this.value;
                }
            }
        });
    };
})(jQuery)


//操作前弹出确认提示
function ShowConfirm(msg, callback) {

    //var mainDoc = getMainDoc(this);
    bootbox.setLocale("zh_CN");//中文

    bootbox.confirm({
        //title:'弹出框标题',
        //show: false, //不显示对话框
        //backdrop:true, //背景是否展示
        //size: large,  //Requires Bootstrap 3.1.0 or newer.
        buttons: {
            confirm: {
                label: '确定',
                className: 'btn btn-danger'
            },
            cancel: {
                label: '取消',
                className: 'btn btn-primary'
            },
        },
        message: msg,
        callback: function(isOk) {
            if (isOk) {
                callback();
            }
        }
    });
}

//操作结果提示
function ShowAlert(msg, callback) {
    //var mainDoc = getMainDoc(this);  //获取底层的窗口
    //mainDoc.bootbox.alert(msg, callback);

    bootbox.setLocale("zh_CN");//中文,此处设置语言失效，原因未知

    bootbox.alert({
        buttons: {
            ok: {
                label: '确定',
            }
        },
        message: msg,
        callback: function () {
            if (callback) {
                callback(); //如果有回调函数就执行
            }
        }
    });

}

function GoToLogin() {
    window.location = '/Home/Index';

}