# -*- encoding: utf-8 -*-
# stub: simple_xlsx_reader 1.0.2 ruby lib

Gem::Specification.new do |s|
  s.name = "simple_xlsx_reader"
  s.version = "1.0.2"

  s.required_rubygems_version = Gem::Requirement.new(">= 0") if s.respond_to? :required_rubygems_version=
  s.require_paths = ["lib"]
  s.authors = ["Woody Peterson"]
  s.date = "2015-02-24"
  s.description = "Read xlsx data the Ruby way"
  s.email = ["woody@sigby.com"]
  s.homepage = ""
  s.rubygems_version = "2.4.5"
  s.summary = "Read xlsx data the Ruby way"

  s.installed_by_version = "2.4.5" if s.respond_to? :installed_by_version

  if s.respond_to? :specification_version then
    s.specification_version = 4

    if Gem::Version.new(Gem::VERSION) >= Gem::Version.new('1.2.0') then
      s.add_runtime_dependency(%q<nokogiri>, [">= 0"])
      s.add_runtime_dependency(%q<rubyzip>, [">= 0"])
      s.add_development_dependency(%q<minitest>, [">= 5.0"])
      s.add_development_dependency(%q<pry>, [">= 0"])
    else
      s.add_dependency(%q<nokogiri>, [">= 0"])
      s.add_dependency(%q<rubyzip>, [">= 0"])
      s.add_dependency(%q<minitest>, [">= 5.0"])
      s.add_dependency(%q<pry>, [">= 0"])
    end
  else
    s.add_dependency(%q<nokogiri>, [">= 0"])
    s.add_dependency(%q<rubyzip>, [">= 0"])
    s.add_dependency(%q<minitest>, [">= 5.0"])
    s.add_dependency(%q<pry>, [">= 0"])
  end
end
