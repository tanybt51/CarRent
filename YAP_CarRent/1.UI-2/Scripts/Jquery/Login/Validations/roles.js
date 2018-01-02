function addRole() {
    var result;
    removeRole();
    var roleName = $('#roles').val();
    var userRoles = $('#userRoles');
    if ($(userRoles).text().length>0)
    result=($(userRoles).text().trim() + ',' + roleName);
    else {
      result= ($(userRoles).text().trim()  + roleName);

    }
    $(userRoles).text(result)
    $('#userRoles1').val(result);
};
function removeRole() {
    var roleName = $('#roles').val();
    var userRoles = $('#userRoles');
    var roles = $(userRoles).text().replace(roleName+',', "");
    var roles = roles.replace(','+roleName, "");
    var roles = roles.replace(roleName, "");
    $(userRoles).text(roles);
    $('#userRoles1').val(roles);

};