#!/bin/bash

SCRIPT_DIR=/opt/oracle/scripts/startup

for f in "$SCRIPT_DIR"/*/*/*; do
    case "$f" in
        *.sql)  echo "$0: runnning $f"; echo "exit" | "$ORACLE_HOME"/bin/sqlplus -s "/ as sysdba" @"$f"; echo ;;
        *)      echo "$0: ignoring $f";;
    esac
    echo ""
done