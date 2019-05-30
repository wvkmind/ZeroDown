# -*- encoding: utf-8 -*-
# stub: mysql2 0.4.5 x64-mingw32 lib

Gem::Specification.new do |s|
  s.name = "mysql2"
  s.version = "0.4.5"
  s.platform = "x64-mingw32"

  s.required_rubygems_version = Gem::Requirement.new(">= 0") if s.respond_to? :required_rubygems_version=
  s.require_paths = ["lib"]
  s.authors = ["Brian Lopez", "Aaron Stone"]
  s.date = "2016-10-22"
  s.email = ["seniorlopez@gmail.com", "aaron@serendipity.cx"]
  s.homepage = "http://github.com/brianmario/mysql2"
  s.licenses = ["MIT"]
  s.post_install_message = "\n======================================================================================================\n\n  You've installed the binary version of mysql2.\n  It was built using MySQL Connector/C version 6.1.6.\n  It's recommended to use the exact same version to avoid potential issues.\n\n  At the time of building this gem, the necessary DLL files were retrieved from:\n  http://cdn.mysql.com/Downloads/Connector-C/mysql-connector-c-6.1.6-win32.zip\n\n  This gem *includes* vendor/libmysql.dll with redistribution notice in vendor/README.\n\n======================================================================================================\n\n"
  s.rdoc_options = ["--charset=UTF-8"]
  s.rubygems_version = "2.4.5.2"
  s.summary = "A simple, fast Mysql library for Ruby, binding to libmysql"

  s.installed_by_version = "2.4.5.2" if s.respond_to? :installed_by_version
end
