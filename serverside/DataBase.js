const mysql = require('mysql');
const fs = require('fs');
const { exit } = require('process');

var profile;
try {
  profile = JSON.parse(fs.readFileSync('./authentication.json', 'utf8'));
} catch (error) {
  console.log(error);
  console.log('file read failed');
  process.exit(1);
}
var con = mysql.createConnection({
  host: profile.host,
  user: profile.user,
  password: profile.password,
  database: profile.database
});

// con.connect(function(err) {
//     if (err) throw err;
//     console.log("Connected!");
//     var sql = "INSERT INTO login (id, pw) VALUES ('Company Inc', 'Highway 37')";
//     con.query(sql, function (err, result) {
//       if (err) throw err;
//       console.log("1 record inserted");
//     });
//   });

con.connect(function(err) {
    if (err) throw err;
    con.query("SELECT * FROM login", function (err, result, fields) {
      if (err) throw err;
      console.log(result);
      //console.log(result.length);
    });
  });