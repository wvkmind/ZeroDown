# -*- encoding: utf-8 -*-
# stub: glu 8.2.2 x64-mingw32 lib

Gem::Specification.new do |s|
  s.name = "glu"
  s.version = "8.2.2"
  s.platform = "x64-mingw32"

  s.required_rubygems_version = Gem::Requirement.new(">= 0") if s.respond_to? :required_rubygems_version=
  s.require_paths = ["lib"]
  s.authors = ["Eric Hodel", "Lars Kanis", "Bla\u{17e} Hrastnik", "Alain Hoang", "Jan Dvorak", "Minh Thu Vo", "James Adam"]
  s.date = "2016-01-21"
  s.description = "Glu bindings for the opengl gem, split into a separate gem because of Glu deprecation."
  s.email = ["drbrain@segment7.net", "", "blaz.hrast@gmail.com", "", "", "", ""]
  s.extra_rdoc_files = ["History.rdoc", "Manifest.txt", "README.rdoc"]
  s.files = ["History.rdoc", "Manifest.txt", "README.rdoc"]
  s.licenses = ["MIT"]
  s.rdoc_options = ["--main", "README.rdoc"]
  s.required_ruby_version = Gem::Requirement.new(">= 1.9.2")
  s.rubygems_version = "2.4.5"
  s.summary = "Glu bindings for the opengl gem, split into a separate gem because of Glu deprecation."

  s.installed_by_version = "2.4.5" if s.respond_to? :installed_by_version

  if s.respond_to? :specification_version then
    s.specification_version = 4

    if Gem::Version.new(Gem::VERSION) >= Gem::Version.new('1.2.0') then
      s.add_development_dependency(%q<rdoc>, ["~> 4.0"])
      s.add_development_dependency(%q<rake-compiler>, [">= 0.9.1", "~> 0.9"])
      s.add_development_dependency(%q<rake-compiler-dock>, ["~> 0.5.0"])
      s.add_development_dependency(%q<hoe>, ["~> 3.14"])
    else
      s.add_dependency(%q<rdoc>, ["~> 4.0"])
      s.add_dependency(%q<rake-compiler>, [">= 0.9.1", "~> 0.9"])
      s.add_dependency(%q<rake-compiler-dock>, ["~> 0.5.0"])
      s.add_dependency(%q<hoe>, ["~> 3.14"])
    end
  else
    s.add_dependency(%q<rdoc>, ["~> 4.0"])
    s.add_dependency(%q<rake-compiler>, [">= 0.9.1", "~> 0.9"])
    s.add_dependency(%q<rake-compiler-dock>, ["~> 0.5.0"])
    s.add_dependency(%q<hoe>, ["~> 3.14"])
  end
end
