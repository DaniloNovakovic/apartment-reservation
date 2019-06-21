export function formatDateToYearMonthDayString(date) {
  const dateTime = new Date(date);
  const dd = dateTime.getDate();
  const mm = dateTime.getMonth() + 1;
  const y = dateTime.getFullYear();

  return y + "/" + mm + "/" + dd;
}

export function areSameDay(firstDate, secondDate) {
  const firstStr = formatDateToYearMonthDayString(firstDate);
  const secondStr = formatDateToYearMonthDayString(secondDate);
  return firstStr === secondStr;
}

export function sortDatesByDay(datesToSort) {
  let dates = datesToSort.map(date => new Date(date));

  dates.sort((lhsDate, rhsDate) => {
    const cmpByYear = lhsDate.getFullYear() - rhsDate.getFullYear();
    if (cmpByYear !== 0) {
      return cmpByYear;
    }

    const cmpByMonth = lhsDate.getMonth() - rhsDate.getMonth();
    if (cmpByMonth !== 0) {
      return cmpByMonth;
    }

    return lhsDate.getDate() - rhsDate.getDate();
  });

  return dates;
}

export function isYesterday(yesterday, today) {
  const yesterdayDate = new Date(yesterday);
  const todayDate = new Date(today);
  yesterdayDate.setDate(yesterdayDate.getDate() + 1);
  return areSameDay(yesterdayDate, todayDate);
}

export function calculateMaxNumberOfNights(selectedDay, availableDays) {
  const selectedDayStr = formatDateToYearMonthDayString(selectedDay);
  const availableDaysSortedStr = sortDatesByDay(availableDays).map(item => {
    return formatDateToYearMonthDayString(item);
  });

  const startIndex = availableDaysSortedStr.lastIndexOf(selectedDayStr);
  if (startIndex < 0) {
    return 0;
  }
  let maxNumberOfNights = 1;
  for (let i = startIndex + 1; i < availableDaysSortedStr.length; ++i) {
    let prevDate = availableDaysSortedStr[i - 1];
    let currDate = availableDaysSortedStr[i];
    if (!isYesterday(prevDate, currDate)) {
      return maxNumberOfNights;
    }
    ++maxNumberOfNights;
  }
  return maxNumberOfNights;
}
