#!/bin/bash

readonly SOLUTION_DIR="src"
readonly API_PROJECT_DIR="$SOLUTION_DIR/Storefront.Ordering.API"
readonly TEST_PROJECT_DIR="$SOLUTION_DIR/Storefront.Ordering.Tests"

main()
{
  sh_command=$1

  if [[ $sh_command ]]
  then
    case $sh_command in
      install) install_dotnet ;;
      test)    run_tests ;;
      clean)   clean ;;
      help)    show_help ;;
      *) echo "'$sh_command' is not recognized as a command. See 'help' for details." ;;
    esac
  else
    echo "See 'help' for details."
  fi
}

show_help()
{
  echo "Project Command Line Tools"
  echo ""
  echo "Commands:"
  echo "  install    Install .Net Core SDK"
  echo "  test       Run project tests and generate coverage report"
  echo "  clean      Clean all projects and restore"
  echo "  help       Show command line help"
}

install_dotnet()
{
  set -e

  tar_gz="https://dotnetcli.blob.core.windows.net/dotnet/Sdk/2.2.401/dotnet-sdk-2.2.401-linux-x64.tar.gz"

  curl -SL -o dotnet.tar.gz $tar_gz
  sudo mkdir -p /usr/share/dotnet
  sudo tar -zxf dotnet.tar.gz -C /usr/share/dotnet
  sudo ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet
}

run_tests()
{
  cd $TEST_PROJECT_DIR

  dotnet test  \
    /p:AltCover="true" \
    /p:AltCoverForce="true" \
    /p:AltCoverThreshold="80" \
    /p:AltCoverOpenCover="true" \
    /p:AltCoverXmlReport="coverage/opencover.xml" \
    /p:AltCoverInputDirectory="$API_PROJECT_DIR" \
    /p:AltCoverAttributeFilter="ExcludeFromCodeCoverage" \
    /p:AltCoverAssemblyExcludeFilter="System(.*)|xunit|$TEST_PROJECT_DIR|$API_PROJECT_DIR.Views"

  dotnet reportgenerator \
    "-reports:coverage/opencover.xml" \
    "-reporttypes:Html;HtmlSummary" \
    "-targetdir:coverage/report"
}

clean()
{
  set -e

  dotnet clean $SOLUTION_DIR

  rm -rf $API_PROJECT_DIR/bin
  rm -rf $API_PROJECT_DIR/obj
  rm -rf $TEST_PROJECT_DIR/bin
  rm -rf $TEST_PROJECT_DIR/obj
  rm -rf $TEST_PROJECT_DIR/coverage

  dotnet restore $SOLUTION_DIR
}

main "$@"
