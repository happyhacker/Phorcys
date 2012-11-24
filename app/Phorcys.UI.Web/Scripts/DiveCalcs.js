function CalcMOD(o2, ppo2, isFreshWater) {
  var atmDepth = 34;
  var isFresh = true;

  for (i = 0; i < isFreshWater.length; i++) {
    if (isFreshWater[i].checked) {
      isFresh = isFreshWater[i].value;
    }
  }
  if (isFresh == "false") {
    atmDepth = 33;
  }
  var mod = (ppo2 / (o2 / 100) - 1) * atmDepth;
  return Math.round(mod);
}

function CalcBoth(formVar) {
  formVar.MOD.value = CalcMOD(formVar.O2.value, formVar.PPO2.value, formVar.IsFreshWater);
  if (formVar.HE.value > 0) {
    formVar.END.value = CalcEND(formVar.HE.value, formVar.MOD.value, true);
  }
  else {
    formVar.END.value = formVar.MOD.value;
  }
  return;
}

function CalcEND(he, depth, isFreshWater) {
  var end;
  var atmDepth = 33;
  if (isFreshWater) atmDepth = 34;

  //end = ( (1 - (he / 100)) * (depth + atmDepth)) - atmDepth;
  var1 = 1 - (he / 100);
  var2 = parseFloat(depth) + parseFloat(atmDepth);
  end = (var1 * var2) - atmDepth;
  return Math.round(end);
}
